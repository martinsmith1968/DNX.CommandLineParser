using DNX.Helpers.Validation;

namespace DNX.CommandLineParser.Errors
{
    public class UnknownOptionError : BaseParserError
    {
        public UnknownOptionError(string optionName)
            : base(BuildMessage(optionName))
        {
        }

        private static string BuildMessage(string optionName)
        {
            Guard.IsNotNullOrEmpty(() => optionName);

            return string.Format("'{0}' is unknown", optionName);
        }
    }
}
