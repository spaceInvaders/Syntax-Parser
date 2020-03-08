using System;
using System.Collections.Generic;

namespace Syntax_Pars
{
    class Convertions
    {
        internal static double ConvertToBinary(decimal input, double roundingPrecision)
        {
            bool isPositive = false;
            if (input >= 0)
                isPositive = true;
            else
                input *= -1;
            int integerPart= (int)Math.Truncate(input);
            decimal fractionalPart = input % integerPart;
            List<int> integerPartlist = new List<int>();
            List<int> fractionalPartlist = new List<int>();
            int integerResult = 0;
            double fractionalResult = 0;
            while (integerPart > 0)
            {
                integerPartlist.Add(integerPart % 2);
                integerPart /= 2;
            }
            integerPartlist.Reverse();
            for (int index = 0; index < integerPartlist.Count; index++)
            {
                integerResult *= 10;
                integerResult += integerPartlist[index];
            }
            for (int index = 0; index < roundingPrecision; index++)
            {
                fractionalPart *= 2;
                fractionalPartlist.Add((int)Math.Truncate(fractionalPart));
                fractionalPart -= Math.Truncate(fractionalPart);
                fractionalResult *= 10;
                fractionalResult += fractionalPartlist[index];
            }
            fractionalResult /= Math.Pow(10, roundingPrecision);
            double binaryResult = integerResult + fractionalResult;
            if (isPositive)
                return binaryResult;
            else
                return binaryResult * (-1);
        }
    }
}
