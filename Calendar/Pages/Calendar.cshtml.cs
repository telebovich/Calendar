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
        public CalendarService Calendar = new CalendarService();

        public void OnGet()
        {
            Calendar.Init();
        }
    }
}