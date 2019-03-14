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
        private readonly int[,] _yearsArray = new int[4, 4];
        private int _currentYear = 0;

        public DecadeService(int decadeBeginYear, int decadeEndYear)
        {
            _decadeBeginYear = decadeBeginYear;
            _decadeEndYear = decadeEndYear;
            _currentYear = decadeBeginYear - 3;

            Validate();

            InititializeYearsArrayWithZeroes();
        }

        private void InititializeYearsArrayWithZeroes()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    _yearsArray[i, j] = 0;
        }

        private void Validate()
        {
            if (_decadeBeginYear % 10 != 0)
                throw new ArgumentException("Start of decade is incorrect");

            if (_decadeEndYear - _decadeBeginYear != 9)
                throw new ArgumentException("End of decade is incorrect");
        }

        public int[,] GetYearsArray()
        {
            FillData();

            return _yearsArray;
        }

        private void FillData()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    FillProperValuesInCell(i, j);
                }
        }

        private void FillProperValuesInCell(int i, int j)
        {
            if (_currentYear >= _decadeBeginYear && _currentYear <= _decadeEndYear)
                _yearsArray[i, j] = _currentYear++;
            else
                _currentYear++;
        }
    }
}
