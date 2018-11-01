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
        public List<int[]> CurrentMonth = new List<int[]>();

        public void OnGet()
        {
            // TODO: Refactor and write some unit tests
            // The code is hardcoded to first day of week is Sunday
            // Calculate metadata
            GregorianCalendar calendar = new GregorianCalendar();
            DateTime now = DateTime.Now;
            int daysInMonth = calendar.GetDaysInMonth(now.Year, now.Month);
            DayOfWeek firstDayOfMonth =
                calendar.GetDayOfWeek(new DateTime(now.Year, now.Month, 1));
            int weeks = daysInMonth / 7;
            if (daysInMonth > 28 
                || (daysInMonth == 28 && firstDayOfMonth != DayOfWeek.Sunday))
                weeks += 1;
            if (firstDayOfMonth == DayOfWeek.Thursday 
                || firstDayOfMonth == DayOfWeek.Friday
                || firstDayOfMonth == DayOfWeek.Saturday)
                weeks += 1;
            int date = 0;

            // Fill month data so we will be able to diplay it on the page
            for (int i = 0; i < weeks; i++)
            {
                // Create a week
                int[] week = new int[7];
                /* 
                 * Prefix operator.
                 * Increase date by one
                 * Get date
                 */
                for (int j = 0; j < 7; j++)
                {
                    if (date < daysInMonth)
                    {
                        if (i > 0 || (i == 0 && j >= (int)firstDayOfMonth))
                            week[j] = ++date;
                    }
                    else
                        week[j] = 0;
                }
                CurrentMonth.Add(week);
            }
        }
    }
}