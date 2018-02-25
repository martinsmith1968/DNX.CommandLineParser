using DNX.CommandLineParser.Options;

namespace DNX.CommandLineParser.Errors
{
    public class BaseOptionError : BaseParserError
    {
        public IOptionDetails OptionDetails { get; protected set; }

        public BaseOptionError(IOptionDetails optionDetails, string message)
            : base(message)
        {
            OptionDetails = optionDetails;
        }
    }
}
