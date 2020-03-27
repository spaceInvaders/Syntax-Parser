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
                bool isPositive = input >= 0;
                decimal absoultinput = Math.Abs(input);
                decimal integerPart = Math.Truncate(absoultinput);
                decimal fractionalPart = absoultinput - integerPart;
                string binaryResult = null;
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
                string fractionalPartString = null;
                if (fractionalPart == 0)
                {
                    fractionalPartString = "0";
                }
                else
                {
                    List<string> fractionalPartlist = new List<string>();
                    for (int index = 0; index < roundingPrecision; index++)
                    {
                        fractionalPart *= 2;
                        fractionalPartlist.Add(Math.Truncate(fractionalPart).ToString());
                        fractionalPart -= Math.Truncate(fractionalPart);
                    }
                    fractionalPartString = String.Join(String.Empty, fractionalPartlist.ToArray());
                    binaryResult = integerPartString + StringExtension.Separator(culture: culture) + fractionalPartString.ToString();
                    binaryResult = binaryResult.TrimEnd('0').TrimEnd(StringExtension.Separator(culture: culture));
                }
                return isPositive ? binaryResult : "-" + binaryResult;
            }
            catch (Exception)
            {
                return "Convertion to binary failed";
            }
        }
    }
}
