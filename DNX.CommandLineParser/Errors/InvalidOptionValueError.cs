using DNX.CommandLineParser.Options;
using DNX.Helpers.Validation;

namespace DNX.CommandLineParser.Errors
{
    public class InvalidOptionValueError : BaseOptionError
    {
        public InvalidOptionValueError(IOptionDetails optionDetails, object value)
            : base(optionDetails, BuildMessage(optionDetails, value))
        {
        }

        private static string BuildMessage(IOptionDetails optionDetails, object value)
        {
            Guard.IsNotNull(() => optionDetails);

            return string.Format("{0} '{1}' value is invalid: '{2}'", optionDetails.OptionType, optionDetails.Name, value);
        }
    }
}
