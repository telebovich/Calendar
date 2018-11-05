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
        public readonly int[,] CurrentMonth = new int[DAYS_IN_WEEK, WEEKS_IN_MONTH];

        public CalendarService()
        {
            for (int j = 0; j < WEEKS_IN_MONTH; j++)
                for (int i = 0; i < DAYS_IN_WEEK; i++)
                    CurrentMonth[i, j] = 0;

            DateTime now = DateTime.Now;
            daysInMonth = calendar.GetDaysInMonth(now.Year, now.Month);
            firstDayOfMonth = calendar.GetDayOfWeek(new DateTime(now.Year, now.Month, 1));
        }

        public void Init()
        {
            // Fill month data so we will be able to display it on the page
            for (int j = 0; j < WEEKS_IN_MONTH; j++)
            {
                /* 
                 * Prefix operator.
                 * Increase date by one
                 * Get date
                 */
                for (int i = 0; i < DAYS_IN_WEEK; i++)
                {
                    FillProperDateInCell(j, i);
                }
            }
        }

        private void FillProperDateInCell(int j, int i)
        {
            if (currentDate < daysInMonth)
            {
                if (j > 0 || (j == 0 && i >= (int)firstDayOfMonth))
                    CurrentMonth[i, j] = ++currentDate;
            }
            else
                CurrentMonth[i, j] = 0;
        }
    }
}
