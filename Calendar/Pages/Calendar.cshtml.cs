using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Calendar.Pages
{
    public class CalendarModel : PageModel
    {
        public const int DAYS_IN_WEEK = 7;
        public const int WEEKS_IN_MONTH = 6;

        public int[,] CurrentMonth = new int[DAYS_IN_WEEK, WEEKS_IN_MONTH];

        public void OnGet()
        {
            for (int j = 0; j < WEEKS_IN_MONTH; j++)
                for (int i = 0; i < DAYS_IN_WEEK; i++)
                    CurrentMonth[i, j] = 0;

            // TODO: Refactor and write some unit tests
            // The code is hardcoded to first day of week is Sunday
            // Calculate metadata
            GregorianCalendar calendar = new GregorianCalendar();
            DateTime now = DateTime.Now;
            int daysInMonth = calendar.GetDaysInMonth(now.Year, now.Month);
            DayOfWeek firstDayOfMonth =
                calendar.GetDayOfWeek(new DateTime(now.Year, now.Month, 1));
            int currentDate = 0;
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
    }
}