using DNX.CommandLineParser.Attributes;
using System;

namespace Test.DNX.CommandLineParser.Samples
{
    public enum OneToFive
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }

    public class BasicOptions
    {
        [Parameter(0, description: "The filename to process", required: true)]
        public string FileName { get; set; }

        [Option("lc", longName: "linecount", description: "How many lines to show", required: false, defaultValue: 10)]
        public int LineCount { get; set; }

        [Option("dt", longName: "datetime", description: "Earliest Timestamp", required: false, defaultValue: "2018-10-15")]
        public DateTime Timestamp { get; set; }

        [Option("otf", "onetofive", description: "One to Five", required: false, defaultValue: OneToFive.Three)]
        public OneToFive OneToFive { get; set; }
    }
}
