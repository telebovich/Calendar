using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Calendar.Services
{
    public class CalendarService
    {
        private const int DAYS_IN_WEEK = 7;
        private const int WEEKS_IN_MONTH = 6;
        private GregorianCalendar calendar = new GregorianCalendar();
        private DayOfWeek firstDayOfMonth;
        private int currentDate = 0;
        private int daysInMonth = 0;
        private DateTime _now = DateTime.Now;
        private readonly int[,] _monthArray = new int[WEEKS_IN_MONTH, DAYS_IN_WEEK];

        public int Year { get; set; } = 0;
        public int Month { get; set; } = 0;

        public CalendarService(int year, int month)
        {
            Year = GetValidYear(year);
            Month = GetValidMonth(month);

            InitializeMonthArrayWithZeroes();
        }

        private void InitializeMonthArrayWithZeroes()
        {
            for (int i = 0; i < WEEKS_IN_MONTH; i++)
                for (int j = 0; j < DAYS_IN_WEEK; j++)
                    _monthArray[i, j] = 0;
        }

        public int[,] GetMonthArray()
        {
            BuildMetadata(Year, Month);

            FillMonthData();

            return _monthArray;
        }

        private int GetValidMonth(int month)
        {
            if ((month < 1 && month != 0) || month > 12)
                throw new ArgumentOutOfRangeException("Please specify year between 1940 and 2240");
            return month == 0 ? _now.Month : month;
        }

        private int GetValidYear(int year)
        {
            if ((year < 1940 && year != 0) || year > 2240)
                throw new ArgumentOutOfRangeException("Please specify year between 1940 and 2240");
            return year == 0 ? _now.Year : year;
        }

        private void BuildMetadata(int year, int month)
        {
            DateTime now = DateTime.Now;
            daysInMonth = calendar.GetDaysInMonth(year, month);
            firstDayOfMonth = calendar.GetDayOfWeek(new DateTime(year, month, 1));
        }

        private void FillMonthData()
        {
            // Fill month data so we will be able to display it on the page
            for (int i = 0; i < WEEKS_IN_MONTH; i++)
            {
                /* 
                 * Prefix operator.
                 * Increase date by one
                 * Get date
                 */
                for (int j = 0; j < DAYS_IN_WEEK; j++)
                {
                    FillProperDateInCell(i, j);
                }
            }
        }

        public string GetMonthName()
        {
            return Convert.ToString((MonthNames)Month);
        }

        private void FillProperDateInCell(int i, int j)
        {
            if (currentDate < daysInMonth)
            {
                if (i > 0 || (i == 0 && j >= (int)firstDayOfMonth))
                    _monthArray[i, j] = ++currentDate;
            }
            else
                _monthArray[i, j] = 0;
        }

        public (int year, int month) GetNextMonth()
        {
            int returnMonth = Month, returnYear = Year;

            if (Month < 12)
            {
                returnMonth += 1;
            }

            if (Month == 12)
            {
                returnYear += 1;
                returnMonth = 1;
            }

            if (Year == 2239 && Month == 12)
            {
                returnYear = 1940;
                returnMonth = 1;
            }

            return (returnYear, returnMonth);
        }

        public (int year, int month) GetPreviousMonth()
        {
            int returnYear = Year, returnMonth = Month;

            if (Month > 1)
            {
                returnMonth -= 1;
            }

            if (Month == 1)
            {
                returnYear -= 1;
                returnMonth = 12;
            }

            if (Year == 1940 && Month == 1)
            {
                returnYear = 2239;
                returnMonth = 12;
            }

            return (returnYear, returnMonth);
        }

        public int GetPreviousYear()
        {
            if (Year > 1940)
                return Year - 1;
            else 
                return 2239;
        }

        public int GetNextYear()
        {
            if (Year < 2239)
                return Year + 1;
            else
                return 1940;
        }
    }
}
