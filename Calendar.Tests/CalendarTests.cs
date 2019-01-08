using Calendar.Services;
using System;
using Xunit;

namespace Calendar.Tests
{
    public class CalendarTests
    {
        [Fact]
        public void Calendar_should_create_correct_month()
        {
            // Arrange
            CalendarService calendar = new CalendarService();

            // Act
            calendar.Init(2018, 11);
            int[,] expected = {
                { 0, 0, 0, 0, 1, 2, 3 },
                { 4, 5, 6, 7, 8, 9, 10 },
                { 11, 12, 13, 14, 15, 16, 17 },
                { 18, 19, 20, 21, 22, 23, 24 },
                { 25, 26, 27, 28, 29, 30, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
            };
            // Assert
            Assert.Equal<int[,]>(expected, calendar.CurrentMonth);
        }

        [Theory]
        [InlineData(int.MinValue,int.MinValue)]
        [InlineData(int.MaxValue,int.MaxValue)]
        [InlineData(2019,int.MinValue)]
        [InlineData(2019,int.MaxValue)]
        public void Calendar_should_throw_ArgumentOutOfRangeException(int year, int month)
        {
            var calendarService = new CalendarService();

            Assert.Throws<ArgumentOutOfRangeException>(new Action(() => calendarService.Init(year, month)));
        }

        [Fact]
        public void Should_not_throw_any_exception()
        {
            var calendarService = new CalendarService();

            calendarService.Init(0, 0);
        }
    }
}
