using System.Collections.Generic;

namespace DNX.CommandLineParser.Configuration
{
    public class ParserConfiguration
    {
        internal bool Writable { get; set; }

        public IList<string> ShortOptionPrefixes { get; set; }

        public IList<string> LongOptionPrefixes { get; set; }

        public bool ThrowOnParseFailure { get; set; }

        public ParserConfiguration()
        {
            Writable = true;

            ShortOptionPrefixes = new List<string>()
            {
                "-",
                "/"
            };
            LongOptionPrefixes = new List<string>()
            {
                "--"
            };
            ThrowOnParseFailure = false;
        }

        public static ParserConfiguration DefaultConfiguration
        {
            get { return new ParserConfiguration();}
        }
    }
}
