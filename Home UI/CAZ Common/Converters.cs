using System;
using System.Collections.Generic;
using System.Text;

namespace CAZ_Common
{
    public static class Converters
    {

        public static double ConvertCurrencyStringToDouble(string item)
        {
            double value = 0;
            bool isNegative = false;
            bool isSuccess = false;

            foreach (string c in item.Split('$'))
            {
                if (c.Equals("-"))
                    isNegative = true;
                else if (!c.Equals("$"))
                    isSuccess = Double.TryParse(c, out value);
            }

            if (!isSuccess)
                return 0;

            if (isNegative)
                value = value * (-1);

            return value;
        }

    }
}
