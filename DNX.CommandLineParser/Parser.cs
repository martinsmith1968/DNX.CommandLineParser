using DNX.CommandLineParser.Configuration;
using DNX.CommandLineParser.Errors;
using DNX.CommandLineParser.Options;
using DNX.Helpers.Strings;
using DNX.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DNX.Helpers.Linq;

namespace DNX.CommandLineParser
{
    public sealed class Parser
    {
        public ParserConfiguration Configuration { get; set; }

        public Parser()
        {
            Configuration = ParserConfiguration.DefaultConfiguration;
        }

        public Parser(ParserConfiguration configuration)
        {
            Guard.IsNotNull(() => configuration);

            Configuration = configuration;
        }

        public Parser(IParserConfigurationCustomisation customiser)
        : this()
        {
            Guard.IsNotNull(() => customiser);

            customiser.Customise(Configuration);
        }

        public Parser(Action<ParserConfiguration> customiser)
            : this()
        {
            Guard.IsNotNull(() => customiser);

            customiser(Configuration);
        }

        public ParserResult<T> Parse<T>(IList<string> args)
            where T : new()
        {
            var optionsInstance = new T();

            var optionDetailsList = OptionDetails.Build(typeof(T));

            var errors = new List<IParserError>();

            Validate(errors, optionDetailsList);
            if (!errors.Any())
            {
                ApplyDefaultValues(errors, optionsInstance, optionDetailsList);
            }

            if (!errors.Any())
            {
                var position = 0;
                IOptionDetails currentOption = null;

                foreach (var arg in args)
                {
                    if (string.IsNullOrEmpty(arg))
                    {
                        continue;
                    }

                    if (currentOption != null)
                    {
                        try
                        {
                            currentOption.SetValue(optionsInstance, arg);
                        }
                        catch (Exception)
                        {
                            errors.Add(new InvalidOptionValueError(currentOption, arg));
                        }

                        currentOption = null;
                        continue;
                    }

                    if (IsLongOption(Configuration, arg))
                    {
                        var optionName = arg.Before("=");
                        currentOption = FindOption(optionDetailsList, optionName);
                        if (currentOption == null)
                        {
                            errors.Add(new UnknownOptionError(arg));
                        }

                    }

                    if (IsShortOption(Configuration, arg))
                    {
                        var optionName = ExtractOptionName(Configuration, arg);

                        currentOption = FindOption(optionDetailsList, optionName);
                        if (currentOption == null)
                        {
                            errors.Add(new UnknownOptionError(optionName));
                        }

                        continue;
                    }

                    // Find Parameter
                    var parameter = FindParameter(optionDetailsList, position);
                    if (parameter == null)
                    {
                        errors.Add(new UnknownParameterError(position));
                        continue;
                    }

                    try
                    {
                        parameter.SetValue(optionsInstance, arg);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        errors.Add(new InvalidOptionValueError(parameter, arg));
                    }

                    ++position;
                }
            }

            var result = new ParserResult<T>(
                optionsInstance,
                Configuration,
                optionDetailsList,
                errors
                );

            return result;
        }

        private void ApplyDefaultValues(IList<IParserError> errors, object optionsInstance, IList<IOptionDetails> optionDetailsList)
        {
            var optionsWithDefaultValues = optionDetailsList
                .Where(od => od.HasDefaultValue)
                .ToList();

            optionsWithDefaultValues
                .ForEach(od =>
                {
                    try
                    {
                        od.SetValue(optionsInstance, od.DefaultValue);
                    }
                    catch (Exception)
                    {
                        errors.Add(new InvalidOptionValueError(od, od.DefaultValue));
                    }
                });
        }

        private void Validate(IList<IParserError> errors, IList<IOptionDetails> optionDetails)
        {
            // TODO: Check for options called the same name, multiple list parameters, etc

        }

        private static string ExtractOptionName(ParserConfiguration configuration, string arg)
        {
            Guard.IsNotNull(() => configuration);

            return IsShortOption(configuration, arg)
                ? ExtractShortOptionName(configuration, arg)
                : IsLongOption(configuration, arg)
                    ? ExtractLongOptionName(configuration, arg)
                    : null;
        }

        private static string ExtractShortOptionName(ParserConfiguration configuration, string arg)
        {
            Guard.IsNotNull(() => configuration);

            var optionName = arg;

            while (configuration.ShortOptionPrefixes.Any(optionName.StartsWith))
            {
                foreach(var prefix in configuration.ShortOptionPrefixes)
                {
                    optionName = optionName.RemoveStartsWith(prefix);
                }
            }

            return optionName;
        }

        private static string ExtractLongOptionName(ParserConfiguration configuration, string arg)
        {
            Guard.IsNotNull(() => configuration);

            var optionName = configuration.LongOptionPrefixes
                .Aggregate((current, iter) => current.RemoveStartsWith(iter))
                .Before("=");

            return optionName;
        }

        private static IList<IOptionDetails> FindOptionOfTypes(IList<IOptionDetails> optionDetails, IList<OptionType> optionTypes)
        {
            Guard.IsNotNull(() => optionDetails);
            Guard.IsNotNull(() => optionTypes);

            var optionsOfType = optionDetails
                .Where(od => optionTypes.Contains(od.OptionType))
                .ToList();

            return optionsOfType;
        }

        private static IOptionDetails FindOption(IList<IOptionDetails> optionDetails, string optionName)
        {
            Guard.IsNotNull(() => optionDetails);
            Guard.IsNotNullOrEmpty(() => optionName);

            var optionsOfType = FindOptionOfTypes(optionDetails, ListExtensions.CreateList(OptionType.Option, OptionType.Switch));

            var option = optionsOfType
                .FirstOrDefault(od => optionName.Equals(od.ShortName));

            if (option == null)
            {
                option = optionsOfType
                    .FirstOrDefault(od => optionName.Equals(od.LongName));
            }

            return option;
        }

        private IOptionDetails FindParameter(IList<IOptionDetails> optionDetails, int position)
        {
            Guard.IsNotNull(() => optionDetails);
            Guard.IsGreaterThanOrEqualTo(() => position, 0);

            var optionsOfType = optionDetails
                .Where(od => od.OptionType == OptionType.Parameter)
                .ToList();

            var option = optionsOfType
                .FirstOrDefault(od => od.Position == position);

            if (option == null)
            {
                option = optionsOfType
                    .FirstOrDefault(od => od.Position <= position && od.IsEnumerable);
            }

            return option;
        }

        private static bool IsShortOption(ParserConfiguration configuration, string arg)
        {
            Guard.IsNotNull(() => configuration);

            return !string.IsNullOrEmpty(arg)
                   && configuration.ShortOptionPrefixes.Any(arg.StartsWith);
        }

        private static bool IsLongOption(ParserConfiguration configuration, string arg)
        {
            Guard.IsNotNull(() => configuration);

            return (!string.IsNullOrEmpty(arg))
                && configuration.LongOptionPrefixes.Any(arg.StartsWith)
                && arg.Contains("=");
        }

        public static Parser DefaultParser
        {
            get { return new Parser(ParserConfiguration.DefaultConfiguration); }
        }
    }
}
