using DNX.CommandLineParser.Options;

namespace DNX.CommandLineParser.Errors
{
    public class UnknownParameterError : BaseParserError
    {
        public UnknownParameterError(int position)
            : base(BuildMessage(position))
        {
        }

        private static string BuildMessage(int position)
        {
            return string.Format("Unexpected {0} at position '{1}'", OptionType.Parameter, position);
        }
    }
}
