using DNX.CommandLineParser.Attributes;
using DNX.Helpers.Converters;
using DNX.Helpers.Linq;
using DNX.Helpers.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DNX.CommandLineParser.Helpers;
using IEnumerable = System.Collections.IEnumerable;

namespace DNX.CommandLineParser.Options
{
    public class OptionDetails : IOptionDetails
    {
        public OptionType OptionType { get; private set; }

        public Type PropertyType
        {
            get { return PropertyInfo.PropertyType; }
        }

        public PropertyInfo PropertyInfo { get; private set; }
        public bool IsEnumerable
        {
            get { return PropertyHelper.IsEnumerable(PropertyInfo); }
        }

        public bool Required { get; private set; }

        public object DefaultValue { get; set; }

        public bool HasDefaultValue { get { return DefaultValue != null; } }

        public string ShortName { get; private set; }

        public string LongName { get; private set; }

        public string Name
        {
            get
            {
                switch (OptionType)
                {
                    case OptionType.Parameter:
                        return Position.ToString();

                    default:
                        return ListExtensions.CreateList(ShortName, LongName)
                            .CoalesceNullOrEmpty();
                }
            }
        }

        public string Description { get; private set; }

        public int Position { get; private set; }

        public void SetValue(object instance, object value)
        {
            PropertyHelper.SetValue(PropertyInfo, instance, value);
        }

        public static IOptionDetails Create(PropertyInfo propertyInfo, ParameterAttribute attribute)
        {
            var optionDetails = new OptionDetails()
            {
                OptionType   = OptionType.Parameter,
                PropertyInfo = propertyInfo,
                Required     = attribute.Required,
                DefaultValue = attribute.DefaultValue,
                ShortName    = null,
                LongName     = null,
                Description  = attribute.Description,
                Position     = attribute.Position,
            };

            return optionDetails;
        }

        public static IOptionDetails Create(PropertyInfo propertyInfo, OptionAttribute attribute)
        {
            var optionDetails = new OptionDetails()
            {
                OptionType   = OptionType.Option,
                PropertyInfo = propertyInfo,
                Required     = attribute.Required,
                DefaultValue = attribute.DefaultValue,
                ShortName    = attribute.ShortName,
                LongName     = attribute.LongName,
                Description  = attribute.Description,
                Position     = attribute.Position,
            };

            return optionDetails;
        }

        public static IList<IOptionDetails> Build(Type type)
        {
            var list = new List<IOptionDetails>();

            var propertyInfos = type.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var parameterAttribute = propertyInfo.GetCustomAttribute<ParameterAttribute>();
                if (parameterAttribute != null)
                {
                    var parameterInfo = OptionDetails.Create(propertyInfo, parameterAttribute);
                    list.Add(parameterInfo);
                    continue;
                }

                var optionAttribute = propertyInfo.GetCustomAttribute<OptionAttribute>();
                if (optionAttribute != null)
                {
                    var optionInfo = OptionDetails.Create(propertyInfo, optionAttribute);
                    list.Add(optionInfo);
                    continue;
                }
            }

            return list;
        }
    }
}
