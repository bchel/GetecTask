using System;
using Xunit;
using GetecTask.Services;

namespace GetecTaskTests
{
    public class WordyFormatterTests
    {
        [Theory]
        [InlineData("0","zero dollars")]
        [InlineData("1", "one dollar")]
        [InlineData("25,1", "twenty-five dollars and ten cents")]
        [InlineData("0,01", "zero dollars and one cent")]
        [InlineData("45 100", "forty-five thousand one hundred dollars")]
        [InlineData("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [InlineData("1000000", "one million dollars")]
        public void Test1(string input, string output)
        {
            var formatter = new WordyFormatter();

            var result = formatter.Parse(input);

            Assert.True(result.Success);
            Assert.Equal(output, result.WordyOutput);
        }

        [Theory]
        [InlineData("1000000000")]
        [InlineData("-1")]
        public void OutOfRangeTest(string input)
        {
            var formatter = new WordyFormatter();

            var result = formatter.Parse(input);

            Assert.False(result.Success);
        }
    }
}
