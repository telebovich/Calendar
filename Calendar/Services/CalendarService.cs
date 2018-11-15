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
        public readonly int[,] CurrentMonth = new int[WEEKS_IN_MONTH, DAYS_IN_WEEK];

        public CalendarService()
        {
            for (int i = 0; i < WEEKS_IN_MONTH; i++)
                for (int j = 0; j < DAYS_IN_WEEK; j++)
                    CurrentMonth[i, j] = 0;

            DateTime now = DateTime.Now;
            daysInMonth = calendar.GetDaysInMonth(now.Year, now.Month);
            firstDayOfMonth = calendar.GetDayOfWeek(new DateTime(now.Year, now.Month, 1));
        }

        public void Init()
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
