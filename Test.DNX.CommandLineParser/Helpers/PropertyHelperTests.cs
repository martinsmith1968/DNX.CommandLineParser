using DNX.CommandLineParser.Helpers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Shouldly;
using Test.DNX.CommandLineParser.Samples;

namespace Test.DNX.CommandLineParser.Helpers
{
    [TestFixture]
    public class PropertyHelperTests
    {
        [TestCase(typeof(string), ExpectedResult = null)]
        [TestCase(typeof(List<string>), ExpectedResult = typeof(IList))]
        [TestCase(typeof(string[]), ExpectedResult = typeof(Array))]
        public Type GetEnumerableType_Tests(Type type)
        {
            return PropertyHelper.GetEnumerableType(type);
        }

        [Test]
        public void SetListValue_Tests()
        {
            // Arrange
            var fileName1 = @"C:\Temp\FileName1.txt";
            var fileName2 = @"C:\Temp\FileName2.txt";
            var instance = new BasicOptionsListParameter()
            {
                FileNames = new List<string>
                {
                    fileName1
                }
            };

            var propertyInfo = instance.GetType().GetProperty("FileNames");

            // Act
            PropertyHelper.SetListValue(propertyInfo, instance, fileName2);

            // Assert
            instance.FileNames.Count.ShouldBe(2);
            instance.FileNames[0].ShouldBe(fileName1);
            instance.FileNames[1].ShouldBe(fileName2);
        }

        [Test]
        public void SetListValue_not_initialised_in_constructor_Tests()
        {
            // Arrange
            var fileName1 = @"C:\Temp\FileName1.txt";
            var fileName2 = @"C:\Temp\FileName2.txt";
            var instance = new BasicOptionsListParameter();

            var propertyInfo = instance.GetType().GetProperty("FileNames");

            // Act
            PropertyHelper.SetListValue(propertyInfo, instance, fileName1);

            // Assert
            instance.FileNames.ShouldNotBeNull();
            instance.FileNames.Count.ShouldBe(1);
            instance.FileNames[0].ShouldBe(fileName1);
        }

        [Test]
        public void SetArrayValue_Tests()
        {
            // Arrange
            var fileName1 = @"C:\Temp\FileName1.txt";
            var fileName2 = @"C:\Temp\FileName2.txt";
            var instance = new BasicOptionsListParameter()
            {
                FileNames = new []
                {
                    fileName1
                }
            };

            var propertyInfo = instance.GetType().GetProperty("FileNames");

            // Act
            PropertyHelper.SetArrayValue(propertyInfo, instance, fileName2);

            // Assert
            instance.FileNames.Count.ShouldBe(2);
            instance.FileNames[0].ShouldBe(fileName1);
            instance.FileNames[1].ShouldBe(fileName2);
        }
    }
}
