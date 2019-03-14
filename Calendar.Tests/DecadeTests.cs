using Calendar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Calendar.Tests
{
    public class DecadeTests
    {
        [Theory]
        [InlineData(2011, 2019)]
        [InlineData(2010, 2018)]
        public void Should_throw_when_parameters_are_incorrect(int decadeBeginYear, int decadeEndYear)
        {
            Assert.Throws<ArgumentException>(new Action(() =>
            {
                var decadeService = new DecadeService(decadeBeginYear, decadeEndYear);
            }));
        }

        [Fact]
        public void Should_return_correct_data()
        {
            var decadeService = new DecadeService(2010, 2019);
            int[,] actual = decadeService.GetYearsArray();
            int[,] expected =
            {
                { 0, 0, 0, 2010 },
                { 2011, 2012, 2013, 2014 },
                { 2015, 2016, 2017, 2018 },
                { 2019, 0, 0, 0 }
            };
            Assert.Equal(expected, actual);
        }
    }
}
