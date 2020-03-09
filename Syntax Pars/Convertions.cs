using System;
using System.Collections.Generic;

namespace Syntax_Pars
{
    class Convertions
    {
        internal static decimal ConvertToBinary(decimal input, double roundingPrecision)
        {
            bool isPositive = false;
            if (input >= 0)
                isPositive = true;
            else
                input *= -1;
            decimal integerPart= (int)Math.Truncate(input);
            decimal fractionalPart = input - integerPart;
            string integerResult = Convert.ToString((int)integerPart, 2);
            List<int> fractionalPartlist = new List<int>();
            double fractionalResult = 0;
            for (int index = 0; index < roundingPrecision; index++)
            {
                fractionalPart *= 2;
                fractionalPartlist.Add((int)Math.Truncate(fractionalPart));
                fractionalPart -= Math.Truncate(fractionalPart);
                fractionalResult *= 10;
                fractionalResult += fractionalPartlist[index];
            }
            fractionalResult /= Math.Pow(10, roundingPrecision);
            decimal binaryResult = Convert.ToDecimal(integerResult) + Convert.ToDecimal(fractionalResult);
            if (isPositive)
                return binaryResult;
            else
                return binaryResult * (-1);
        }
    }
}
