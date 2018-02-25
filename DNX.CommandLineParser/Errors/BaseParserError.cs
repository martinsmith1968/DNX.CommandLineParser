namespace DNX.CommandLineParser.Errors
{
    public abstract class BaseParserError : IParserError
    {
        public string Message { get; protected set; }

        protected BaseParserError()
            : this(string.Empty)
        {
        }

        protected BaseParserError(string message)
        {
            Message = message;
        }
    }
}
