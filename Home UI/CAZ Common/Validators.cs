using System;
using System.Collections.Generic;
using System.Text;

namespace CAZ_Common
{
    public static class Validators
    {
        public static bool IsStringValidDecimal(string input)
        {
            Decimal result = new Decimal();

            return Decimal.TryParse(input, out result);
        }
    }
}
