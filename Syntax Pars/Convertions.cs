using System;
using System.Collections.Generic;

namespace Syntax_Pars
{
    class Convertions
    {
        internal static int ConvertToBinary(decimal input)
        {
            int number = Decimal.ToInt32(input);
            List<int> list = new List<int>();
            int binaryResult = 0;
            while (number > 0)
            {
                list.Add(number % 2);
                number /= 2;
            }
            list.Reverse();
            for (int index = 0; index < list.Count; index++)
            {
                binaryResult *= 10;
                binaryResult += list[index];
            }
            return binaryResult;
        }
    }
}
