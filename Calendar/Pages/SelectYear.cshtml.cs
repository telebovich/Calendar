using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calendar.Pages
{
    public class SelectYearModel : PageModel
    {
        public int DecadeBeginYear { get; set; }
        public int DecadeEndYear { get; set; }
        public int[,] YearsArray { get; private set; }


        public void OnGet(int decadeBeginYear, int decadeEndYear)
        {
            DecadeBeginYear = decadeBeginYear;
            DecadeEndYear = decadeEndYear;

            var decadeService = new DecadeService(decadeBeginYear, decadeEndYear);
            YearsArray = decadeService.GetYearsArray();
        }
    }
}