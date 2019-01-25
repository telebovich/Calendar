using Calendar.Services;
using System;
using Xunit;

namespace Calendar.Tests
{
    public class CalendarTests
    {
        [Fact]
        public void Calendar_should_be_constructed_with_default_date_passed()
        {
            var calendar = new CalendarService(0, 0);

            DateTime now = DateTime.Now;

            Assert.Equal(now.Year, calendar.Year);
            Assert.Equal(now.Month, calendar.Month);
        }

        [Fact]
        public void Calendar_should_be_constructed_with_date_passed()
        {
            var calendar = new CalendarService(2018, 11);

            Assert.Equal(2018, calendar.Year);
            Assert.Equal(11, calendar.Month);
        }

        [Fact]
        public void Calendar_should_create_correct_month()
        {
            // Arrange
            CalendarService calendar = new CalendarService(2018, 11);

            // Act
            int [,] actual = calendar.GetMonthArray();
            int[,] expected = {
                { 0, 0, 0, 0, 1, 2, 3 },
                { 4, 5, 6, 7, 8, 9, 10 },
                { 11, 12, 13, 14, 15, 16, 17 },
                { 18, 19, 20, 21, 22, 23, 24 },
                { 25, 26, 27, 28, 29, 30, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
            };
            // Assert
            Assert.Equal<int[,]>(expected, actual);
        }

        [Theory]
        [InlineData(int.MinValue,int.MinValue)]
        [InlineData(int.MaxValue,int.MaxValue)]
        [InlineData(2019,int.MinValue)]
        [InlineData(2019,int.MaxValue)]
        public void Calendar_should_throw_ArgumentOutOfRangeException(int year, int month)
        {
            Assert.Throws<ArgumentOutOfRangeException>(new Action(() =>
            {
                var calendarService = new CalendarService(year, month);
            }));
        }

        [Fact]
        public void Should_not_throw_any_exception()
        {
            var calendarService = new CalendarService(0, 0);

            calendarService.GetMonthArray();
        }

        [Theory]
        [InlineData(2019, 1, "January")]
        [InlineData(2019, 2, "February")]
        [InlineData(2019, 3, "March")]
        [InlineData(2019, 4, "April")]
        [InlineData(2019, 5, "May")]
        [InlineData(2019, 6, "June")]
        [InlineData(2019, 7, "July")]
        [InlineData(2019, 8, "August")]
        [InlineData(2019, 9, "September")]
        [InlineData(2019, 10, "October")]
        [InlineData(2019, 11, "November")]
        [InlineData(2019, 12, "December")]
        public void Should_convert_month_number_to_text(int year, int month, string expected)
        {
            var calendarService = new CalendarService(year, month);

            string actual = calendarService.GetMonthName();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2019, 1, 2019, 2)]
        [InlineData(2019, 12, 2020, 1)]
        [InlineData(2239, 12, 1940, 1)]
        public void Should_get_next_month(int year, int month, int expectedYear, int expectedMonth)
        {
            var calendarService = new CalendarService(year, month);
            (int actualYear, int actualMonth) = calendarService.GetNextMonth();
            Assert.Equal(expectedYear, actualYear);
            Assert.Equal(expectedMonth, actualMonth);
        }

        [Theory]
        [InlineData(2019, 1, 2018, 12)]
        [InlineData(2019, 2, 2019, 1)]
        [InlineData(1940, 1, 2239, 12)]
        public void Should_get_previous_month(int year, int month, int expectedYear, int expectedMonth)
        {
            var calendarService = new CalendarService(year, month);
            (int actualYear, int actualMonth) = calendarService.GetPreviousMonth();
            Assert.Equal(expectedYear, actualYear);
            Assert.Equal(expectedMonth, actualMonth);
        }
    }
}
