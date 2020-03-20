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
                bool isPositive = false;
                if (input >= 0)
                    isPositive = true;
                else
                    input *= -1;
                decimal integerPart = Math.Truncate(input);
                decimal fractionalPart = input - integerPart;

                List<string> integerPartlist = new List<string>();
                string integerPartString = null;
                if (integerPart == 0)
                {
                    integerPartString = "0";
                }
                else
                {
                    while (integerPart > 0)
                    {
                        decimal remainder = integerPart - (Math.Truncate(integerPart / 2) * 2);
                        integerPartlist.Add(Convert.ToString(remainder));
                        integerPart = Math.Truncate(integerPart / 2);
                    }
                    integerPartlist.Reverse();
                    integerPartString = String.Join(String.Empty, integerPartlist.ToArray());
                }
               
                List<string> fractionalPartlist = new List<string>();
                for (int index = 0; index < roundingPrecision; index++)
                {
                    fractionalPart *= 2;
                    fractionalPartlist.Add(Convert.ToString(Math.Truncate(fractionalPart)));
                    fractionalPart -= Math.Truncate(fractionalPart);
                }
                string fractionalResult = String.Join(String.Empty, fractionalPartlist.ToArray());
                string binaryResult = integerPartString + StringExtension.Separator(culture: culture) + Convert.ToString(fractionalResult);
                while (binaryResult.EndsWith('0'))
                {
                    binaryResult = binaryResult[0..^1];
                    if (binaryResult.EndsWith(StringExtension.Separator(culture: culture)))
                    {
                        binaryResult = binaryResult[0..^1];
                        break;
                    }
                }
                if (isPositive)
                    return binaryResult;
                else
                    return "-" + binaryResult;
            }
            catch (Exception)
            {
                return "Convertion to binary failed";
            }
        }
    }
}
