using DNX.CommandLineParser.Options;
using DNX.Helpers.Validation;

namespace DNX.CommandLineParser.Errors
{
    public class RequiredOptionMissingError : BaseOptionError
    {
        public RequiredOptionMissingError(IOptionDetails optionDetails)
            : base(optionDetails, BuildMessage(optionDetails))
        {
            OptionDetails = optionDetails;
        }

        private static string BuildMessage(IOptionDetails optionDetails)
        {
            Guard.IsNotNull(() => optionDetails);

            return string.Format("{0}: '{1}' is required", optionDetails.OptionType, optionDetails.Name);
        }
    }
}
