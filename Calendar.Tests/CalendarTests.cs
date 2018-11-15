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
            calendar.Init();
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
    }
}
