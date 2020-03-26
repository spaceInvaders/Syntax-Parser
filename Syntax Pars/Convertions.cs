using System;
using System.Collections.Generic;
using System.Globalization;

namespace Syntax_Pars
{
    class Convertions
    {
        internal static string ConvertDecimalToBinaryString(decimal input, int roundingPrecision, CultureInfo culture)
        {
            try
            {
                bool isPositive = input >= 0 ? true : false;
                input = input >= 0 ? input : input = Math.Abs(input);
                decimal integerPart = Math.Truncate(input);
                decimal fractionalPart = input - integerPart;

                string integerPartString = null;
                if (integerPart == 0)
                {
                    integerPartString = "0";
                }
                else
                {
                    List<string> integerPartlist = new List<string>();
                    while (integerPart > 0)
                    {
                        decimal remainder = integerPart - (Math.Truncate(integerPart / 2) * 2);
                        integerPartlist.Add(remainder.ToString());
                        integerPart = Math.Truncate(integerPart / 2);
                    }
                    integerPartlist.Reverse();
                    integerPartString = String.Join(String.Empty, integerPartlist.ToArray());
                }
               
                List<string> fractionalPartlist = new List<string>();
                for (int index = 0; index < roundingPrecision; index++)
                {
                    fractionalPart *= 2;
                    fractionalPartlist.Add(Math.Truncate(fractionalPart).ToString());
                    fractionalPart -= Math.Truncate(fractionalPart);
                }
                string fractionalResult = String.Join(String.Empty, fractionalPartlist.ToArray());
                string binaryResult = integerPartString + StringExtension.Separator(culture: culture) + fractionalResult.ToString();
                binaryResult = binaryResult.TrimEnd('0').TrimEnd(StringExtension.Separator(culture: culture));
                return isPositive == true ?  binaryResult : "-" + binaryResult;
            }
            catch (Exception)
            {
                return "Convertion to binary failed";
            }
        }
    }
}
