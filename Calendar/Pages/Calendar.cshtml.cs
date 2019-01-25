using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using Calendar.Services;

namespace Calendar.Pages
{
    public class CalendarModel : PageModel
    {
        private int[,] _monthArray;

        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; private set; }
        public int[,] MonthArray => _monthArray;

        public Dictionary<string, string> DownLinkParameters = new Dictionary<string, string>();

        public void OnGet(int year, int month)
        {
            var calendar = new CalendarService(year, month);
            _monthArray = calendar.GetMonthArray();
            Year = calendar.Year;
            Month = calendar.Month;
            MonthName = calendar.GetMonthName();

            (int nextYear, int nextMonth) = calendar.GetNextMonth();

            DownLinkParameters.Add("year", nextYear.ToString());
            DownLinkParameters.Add("month", nextMonth.ToString());
        }
    }
}