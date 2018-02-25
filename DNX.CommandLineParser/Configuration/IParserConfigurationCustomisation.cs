using System;

namespace DNX.CommandLineParser.Configuration
{
    public interface IParserConfigurationCustomisation
    {
        void Customise(ParserConfiguration configuration);
    }
}
