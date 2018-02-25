using System;

namespace DNX.CommandLineParser.Attributes
{
    public abstract class BaseAttribute : Attribute
    {
        public bool Required { get; protected set; }

        public object DefaultValue { get; protected set; }

        public string Description { get; protected set; }

        public int Position { get; protected set; }
    }
}
