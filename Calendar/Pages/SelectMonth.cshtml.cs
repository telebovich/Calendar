using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calendar.Pages
{
    public class SelectMonthModel : PageModel
    {
        public int Year { get; set; }
        public int PreviousYear { get; set; }
        public int NextYear { get; set; }

        public void OnGet(int year)
        {
            var calendarService = new CalendarService(year, 1);

            Year = year;
            PreviousYear = calendarService.GetPreviousYear();
            NextYear = calendarService.GetNextYear();
        }
    }
}