namespace DNX.CommandLineParser.Attributes
{
    public class ParameterAttribute : BaseAttribute
    {
        public ParameterAttribute(
            int position,
            string description  = null,
            bool required       = false,
            object defaultValue = null
            )
        {
            Required     = required;
            Description  = description;
            Position     = position;
            DefaultValue = defaultValue;
        }
    }
}
