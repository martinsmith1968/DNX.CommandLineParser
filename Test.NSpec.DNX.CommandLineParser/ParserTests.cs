using DNX.CommandLineParser;
using NSpec;
using Shouldly;
using System;
using Test.NSpec.DNX.CommandLineParser.Samples;

// ReSharper disable InconsistentNaming

namespace Test.NSpec.DNX.CommandLineParser
{
    public class ParserTests
    {
        public class ParserTests_Parse_BasicOptions : nspec
        {
            private Parser _parser;
            private string[] _args;
            private ParserResult<BasicOptions> _result;

            public void when_called()
            {
                before = () => { _parser = Parser.DefaultParser; };

                act = () => { _result = _parser.Parse<BasicOptions>(_args); };

                context["Given a valid command line for Basic Options"] = () =>
                {
                    const string fileName = @"C:\Temp\MyFileName.txt";
                    const int lineCount = 25;
                    var dateTime = new DateTime(2018, 08, 11);
                    const OneToFive oneToFive = OneToFive.Four;

                    before = () =>
                    {
                        _args = new[]
                        {
                            fileName,
                            "-lc", lineCount.ToString(),
                            "-dt", dateTime.ToString("yyyy-MM-dd"),
                            "-otf", oneToFive.ToString()
                        };
                    };

                    it["should return a valid result"] = () => _result.ShouldNotBeNull();

                    it["should return an Options instance"] = () => _result.Options.ShouldNotBeNull();

                    it["should map FileName correctly"] = () => _result.Options.FileName.ShouldBe(fileName);
                    it["should map LineCount correctly"] = () => _result.Options.LineCount.ShouldBe(lineCount);
                    it["should return Timestamp correctly"] = () => _result.Options.Timestamp.ShouldBe(dateTime);
                    it["should return OneToFive correctly"] = () => _result.Options.OneToFive.ShouldBe(OneToFive.Four);
                };

                context["Given a valid command line for Basic Options with positional Parameter at the end"] = () =>
                {
                    const string fileName = @"C:\Temp\MyFileName.txt";
                    const int lineCount = 25;
                    var dateTime = new DateTime(2018, 08, 11);
                    const OneToFive oneToFive = OneToFive.Four;

                    before = () =>
                    {
                        _args = new[]
                        {
                            "-lc", lineCount.ToString(),
                            "-dt", dateTime.ToString("yyyy-MM-dd"),
                            "-otf", oneToFive.ToString(),
                            fileName
                        };
                    };

                    it["should return a valid result"] = () => _result.ShouldNotBeNull();

                    it["should return an Options instance"] = () => _result.Options.ShouldNotBeNull();

                    it["should map FileName correctly"] = () => _result.Options.FileName.ShouldBe(fileName);
                    it["should map LineCount correctly"] = () => _result.Options.LineCount.ShouldBe(lineCount);
                    it["should return Timestamp correctly"] = () => _result.Options.Timestamp.ShouldBe(dateTime);
                    it["should return OneToFive correctly"] = () => _result.Options.OneToFive.ShouldBe(OneToFive.Four);
                };

                context["Given a valid command line for Basic Options with positional Parameter in the middle"] = () =>
                {
                    const string fileName = @"C:\Temp\MyFileName.txt";
                    const int lineCount = 25;
                    var dateTime = new DateTime(2018, 08, 11);
                    const OneToFive oneToFive = OneToFive.Four;

                    before = () =>
                    {
                        _args = new[]
                        {
                            "-lc", lineCount.ToString(),
                            "-dt", dateTime.ToString("yyyy-MM-dd"),
                            "-otf", oneToFive.ToString(),
                            fileName
                        };
                    };

                    it["should return a valid result"] = () => _result.ShouldNotBeNull();

                    it["should return an Options instance"] = () => _result.Options.ShouldNotBeNull();

                    it["should map FileName correctly"] = () => _result.Options.FileName.ShouldBe(fileName);
                    it["should map LineCount correctly"] = () => _result.Options.LineCount.ShouldBe(lineCount);
                    it["should return Timestamp correctly"] = () => _result.Options.Timestamp.ShouldBe(dateTime);
                    it["should return OneToFive correctly"] = () => _result.Options.OneToFive.ShouldBe(OneToFive.Four);
                };
            }
        }

        public class ParserTests_Parse_BasicOptionsEnumerableParameter : nspec
        {
            private Parser _parser;
            private string[] _args;
            private ParserResult<BasicOptionsEnumerableParameter> _result;

            public void when_called()
            {
                before = () => { _parser = Parser.DefaultParser; };

                act = () => { _result = _parser.Parse<BasicOptionsEnumerableParameter>(_args); };

                context["Given a valid command line for Basic Options"] = () =>
                {
                    const string fileName1 = @"C:\Temp\MyFileName1.txt";
                    const string fileName2 = @"C:\Temp\MyFileName1.txt";
                    const string fileName3 = @"C:\Temp\MyFileName1.txt";
                    const int lineCount = 25;
                    var dateTime = new DateTime(2018, 08, 11);
                    const OneToFive oneToFive = OneToFive.Four;

                    before = () =>
                    {
                        _args = new[]
                        {
                            fileName1,
                            fileName2,
                            fileName3,
                            "-lc", lineCount.ToString(),
                            "-dt", dateTime.ToString("yyyy-MM-dd"),
                            "-otf", oneToFive.ToString()
                        };
                    };

                    it["should return a valid result"] = () => _result.ShouldNotBeNull();

                    it["should return an Options instance"] = () => _result.Options.ShouldNotBeNull();

                    it["should map FileNames correctly"] = () => _result.Options.FileNames.Count.ShouldBe(3);
                    it["should map FileName1 correctly"] = () => _result.Options.FileNames[0].ShouldBe(fileName1);
                    it["should map FileName2 correctly"] = () => _result.Options.FileNames[1].ShouldBe(fileName2);
                    it["should map FileName3 correctly"] = () => _result.Options.FileNames[2].ShouldBe(fileName3);
                    it["should map LineCount correctly"] = () => _result.Options.LineCount.ShouldBe(lineCount);
                    it["should return Timestamp correctly"] = () => _result.Options.Timestamp.ShouldBe(dateTime);
                    it["should return OneToFive correctly"] = () => _result.Options.OneToFive.ShouldBe(OneToFive.Four);
                };
            }
        }
    }
}
