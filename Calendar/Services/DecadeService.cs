using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Services
{
    public class DecadeService
    {
        private readonly int _decadeBeginYear = 0;
        private readonly int _decadeEndYear = 0;

        public DecadeService(int decadeBeginYear, int decadeEndYear)
        {
            _decadeBeginYear = decadeBeginYear;
            _decadeEndYear = decadeEndYear;

            Validate();
        }

        private void Validate()
        {
            if (_decadeBeginYear % 10 != 0)
                throw new ArgumentException("Start of decade is incorrect");

            if (_decadeEndYear - _decadeBeginYear != 9)
                throw new ArgumentException("End of decade is incorrect");
        }
    }
}
