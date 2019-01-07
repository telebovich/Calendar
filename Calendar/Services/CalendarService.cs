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
        public readonly int[,] CurrentMonth = new int[WEEKS_IN_MONTH, DAYS_IN_WEEK];

        public CalendarService()
        {
            for (int i = 0; i < WEEKS_IN_MONTH; i++)
                for (int j = 0; j < DAYS_IN_WEEK; j++)
                    CurrentMonth[i, j] = 0;
        }

        public void Init(int year, int month)
        {
            ValidateInputParameters(year, month);

            if (year == 0)
                year = _now.Year;
            if (month == 0)
                month = _now.Month;

            BuildMetadata(year, month);

            FillMonthData();
        }

        private void ValidateInputParameters(int year, int month)
        {
            bool valid = true;
            if (year < 1940 || year > 2240)
                valid = false;
            if (year == 0 && month == 0)
                valid = true;
            if (!valid)
                throw new ArgumentOutOfRangeException("Please enter year between 1940 and 2240");
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

        private void FillProperDateInCell(int i, int j)
        {
            if (currentDate < daysInMonth)
            {
                if (i > 0 || (i == 0 && j >= (int)firstDayOfMonth))
                    CurrentMonth[i, j] = ++currentDate;
            }
            else
                CurrentMonth[i, j] = 0;
        }
    }
}
