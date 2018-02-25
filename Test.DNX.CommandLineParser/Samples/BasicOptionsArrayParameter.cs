using DNX.CommandLineParser.Attributes;
using System;

namespace Test.DNX.CommandLineParser.Samples
{
    public class BasicOptionsArrayParameter
    {
        [Parameter(0, description: "The filename to process", required: true)]
        public string[] FileNames { get; set; }

        [Option("lc", longName: "linecount", description: "How many lines to show", required: false, defaultValue: 10)]
        public int LineCount { get; set; }

        [Option("dt", longName: "datetime", description: "Timestamp", required: false, defaultValue: "2018-10-15")]
        public DateTime Timestamp { get; set; }

        [Option("otf", "onetofive", description: "One to Five", required: false, defaultValue: OneToFive.Three)]
        public OneToFive OneToFive { get; set; }
    }
}
