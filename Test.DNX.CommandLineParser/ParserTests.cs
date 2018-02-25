using System;
using System.Linq;
using DNX.CommandLineParser;
using NUnit.Framework;
using Shouldly;
using Test.DNX.CommandLineParser.Samples;

namespace Test.DNX.CommandLineParser
{
    [TestFixture]
    public class ParserTests
    {
        public class ParserTests_BasicOptions
        {
            [Test]
            public void Can_parse_commandline_successfully()
            {
                // Arrange
                var fileName = @"C:\Temp\MyFileName.txt";
                var lineCount = 25;
                var dateTime = DateTime.Today;

                var args = new[]
                {
                    fileName,
                    "-lc", lineCount.ToString(),
                    "-dt", dateTime.ToString("yyyy-MM-dd"),
                    "-otf", OneToFive.Four.ToString()
                };

                // Act
                var result = Parser.DefaultParser.Parse<BasicOptions>(args);

                // Assert
                result.ShouldNotBeNull();
                result.Errors.ToList().ForEach(e => Console.WriteLine(e.Message));
                result.Success.ShouldBeTrue();

                result.Options.ShouldNotBeNull();
                result.Options.FileName.ShouldBe(fileName);
                result.Options.LineCount.ShouldBe(lineCount);
                result.Options.Timestamp.ShouldBe(dateTime);
                result.Options.OneToFive.ShouldBe(OneToFive.Four);
            }

            [Test] public void Can_parse_commandline_when_Parameters_reversed_successfully()
            {
                // Arrange
                var fileName = @"C:\Temp\MyFileName.txt";
                var lineCount = 25;
                var dateTime = DateTime.Today;

                var args = new[]
                {
                    "-lc", lineCount.ToString(),
                    "-lc", lineCount.ToString(),
                    "-dt", dateTime.ToString("yyyy-MM-dd"),
                    "-otf", "4",
                    fileName
                };

                // Act
                var result = Parser.DefaultParser.Parse<BasicOptions>(args);

                // Assert
                result.ShouldNotBeNull();
                result.Errors.ToList().ForEach(e => Console.WriteLine(e.Message));
                result.Success.ShouldBeTrue();

                result.Options.ShouldNotBeNull();
                result.Options.FileName.ShouldBe(fileName);
                result.Options.LineCount.ShouldBe(lineCount);
                result.Options.Timestamp.ShouldBe(dateTime);
                result.Options.OneToFive.ShouldBe(OneToFive.Four);
            }
        }

        public class ParserTests_BasicOptionsEnumerableParameters
        {
            [Test]
            public void Can_parse_commandline_successfully()
            {
                // Arrange
                var fileName1 = @"C:\Temp\MyFileName1.txt";
                var fileName2 = @"C:\Temp\MyFileName2.txt";
                var fileName3 = @"C:\Temp\MyFileName3.txt";
                var lineCount = 25;
                var dateTime = DateTime.Today;

                var args = new[]
                {
                    fileName1,
                    fileName2,
                    fileName3,
                    "-lc", lineCount.ToString(),
                    "-dt", dateTime.ToString("yyyy-MM-dd"),
                    "-otf", OneToFive.Four.ToString()
                };

                // Act
                var result = Parser.DefaultParser.Parse<BasicOptionsListParameter>(args);

                // Assert
                result.ShouldNotBeNull();
                result.Errors.ToList().ForEach(e => Console.WriteLine(e.Message));
                result.Success.ShouldBeTrue();

                result.Options.ShouldNotBeNull();

                result.Options.FileNames.Count.ShouldBe(3);
                result.Options.FileNames[0].ShouldBe(fileName1);
                result.Options.FileNames[1].ShouldBe(fileName2);
                result.Options.FileNames[2].ShouldBe(fileName3);
                result.Options.LineCount.ShouldBe(lineCount);
                result.Options.Timestamp.ShouldBe(dateTime);
                result.Options.OneToFive.ShouldBe(OneToFive.Four);
            }

            [Test]
            public void Can_parse_commandline_when_Parameters_reversed_successfully()
            {
                // Arrange
                var fileName1 = @"C:\Temp\MyFileName1.txt";
                var fileName2 = @"C:\Temp\MyFileName2.txt";
                var fileName3 = @"C:\Temp\MyFileName3.txt";
                var lineCount = 25;
                var dateTime = DateTime.Today;

                var args = new[]
                {
                    "-lc", lineCount.ToString(),
                    "-lc", lineCount.ToString(),
                    "-dt", dateTime.ToString("yyyy-MM-dd"),
                    "-otf", "4",
                    fileName1,
                    fileName2,
                    fileName3
                };

                // Act
                var result = Parser.DefaultParser.Parse<BasicOptionsListParameter>(args);

                // Assert
                result.ShouldNotBeNull();
                result.Errors.ToList().ForEach(e => Console.WriteLine(e.Message));
                result.Success.ShouldBeTrue();

                result.Options.FileNames.Count.ShouldBe(3);
                result.Options.FileNames[0].ShouldBe(fileName1);
                result.Options.FileNames[1].ShouldBe(fileName2);
                result.Options.FileNames[2].ShouldBe(fileName3);
                result.Options.LineCount.ShouldBe(lineCount);
                result.Options.Timestamp.ShouldBe(dateTime);
                result.Options.OneToFive.ShouldBe(OneToFive.Four);
            }
        }
    }
}
