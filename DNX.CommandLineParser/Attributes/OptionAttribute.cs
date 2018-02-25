namespace DNX.CommandLineParser.Attributes
{
    public class OptionAttribute : BaseAttribute
    {
        public string ShortName { get; private set; }

        public string LongName { get; set; }

        public OptionAttribute(
            string shortName,
            string longName     = null,
            string description  = null,
            bool required       = false,
            object defaultValue = null,
            int position        = 0
            )
        {
            ShortName    = shortName;
            LongName     = longName;
            Description  = description;
            Required     = required;
            DefaultValue = defaultValue;
            Position     = position;
        }
    }
}
