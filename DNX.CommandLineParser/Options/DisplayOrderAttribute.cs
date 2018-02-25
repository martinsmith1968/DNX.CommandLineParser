using System;

namespace DNX.CommandLineParser.Options
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayOrderAttribute : Attribute
    {
        public int Sequence { get; set; }

        public DisplayOrderAttribute(int sequence)
        {
            Sequence = sequence;
        }
    }
}
