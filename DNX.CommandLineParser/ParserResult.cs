using DNX.CommandLineParser.Configuration;
using DNX.CommandLineParser.Errors;
using DNX.CommandLineParser.Options;
using System.Collections.Generic;
using System.Linq;

namespace DNX.CommandLineParser
{
    public class ParserResult<T>
        where T : new()
    {
        public T Options { get; private set; }

        public ParserConfiguration Configuration { get; private set; }

        public IList<IOptionDetails> OptionDetailsList { get; private set; }

        public IParserError[] Errors { get; private set; }

        public bool Success
        {
            get { return !Errors.Any(); }
        }

        internal ParserResult
        (
            T options,
            ParserConfiguration configuration,
            IList<IOptionDetails> optionDetailsList,
            IEnumerable<IParserError> errors
            )
        {
            Options           = options;
            Configuration     = configuration;
            OptionDetailsList = optionDetailsList;
            Errors            = (errors ?? Enumerable.Empty<IParserError>())
                .ToArray();
        }
    }
}
