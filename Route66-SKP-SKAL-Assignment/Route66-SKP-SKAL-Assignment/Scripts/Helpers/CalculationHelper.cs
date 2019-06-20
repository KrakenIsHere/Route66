using System;

namespace Route66_SKP_SKAL_Assignment.Scripts.Helpers
{
    public class CalculationHelper
    {
        public static int[] GetMonthIdForQuestion(int sm, int sy)
        {
            var resultId = new int[3];

            var date = DateTime.Now;
            var start = DateTime.Parse($"1 {sm} {sy}");

            var monthId = (((date.Year - start.Year) * 12) + date.Month - start.Month) + 1;

            var calc = 1;

            if (monthId != 1)
            {
                calc = 2 * monthId;
            }

            resultId[0] = 0 + calc;
            resultId[1] = 1 + calc;
            resultId[2] = 2 + calc;

            return resultId;
        }
    }
}