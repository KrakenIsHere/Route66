using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Route66_SKP_SKAL_Assignment.Scripts.Helpers
{
    public class CalculationHelper
    {
        public int[] GetMonthIdForQuestion(int SM, int SY)
        {
            int[] resultId = new int[3];

            DateTime date = DateTime.Now;
            DateTime start = DateTime.Parse($"1 {SM} {SY}");

            int monthId = (((date.Year - start.Year) * 12) + date.Month - start.Month) + 1;

            int calc = 1;

            if (monthId != 1)
            {
                calc = (2 * monthId);
            }

            resultId[0] = 0 + calc;
            resultId[1] = 1 + calc;
            resultId[2] = 2 + calc;

            return resultId;
        }
    }
}