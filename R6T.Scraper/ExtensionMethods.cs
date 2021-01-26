using System;
using System.Collections.Generic;
using System.Text;

namespace R6T.Scraper
{
    public static class ExtensionMethods
    {
        public static Int32 ToInt32(this string value)
        {
            var intVal = 0;
            value = value.PrepareForNumberConversion();
            Int32.TryParse(value, out intVal);
            return intVal;
        }

        public static Decimal ToDecimal(this string value)
        {
            Decimal decimalVal = Decimal.Zero;
            Decimal.TryParse(value, out decimalVal);
            return decimalVal;
        }


        public static string PrepareForNumberConversion(this string value)
        {
            if (value.Contains(","))
            {
                value = value.Replace(",", "");
            }

            return value;
        }
    }
}
