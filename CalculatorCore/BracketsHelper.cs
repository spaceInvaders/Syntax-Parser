using System;
using System.Linq;

namespace CalculatorCore
{
    internal static class BracketsHelper
    {
        /// <summary> Removes the outer brackets </summary>
        internal static string TrimBrackets(string input) 
        {
            if (input.StartsWith(CalculationConstants.OpeningBracket) &&
                input.EndsWith(CalculationConstants.ClosingBracket))
            {
                var bracketsLevel = BracketsLevel(input: input);

                for (var index = 1; index < input.Length - 1; index++)
                {
                    if (bracketsLevel[index] == 0)
                    {
                        return input;
                    }
                    else if (index == input.Length - 2)
                    {
                        input = input[1..^1];
                    }
                }
            }

            if (input.StartsWith(CalculationConstants.OpeningBracket) &&
                input.EndsWith(CalculationConstants.ClosingBracket))
            {
                input = TrimBrackets(input);
            }

            return input;
        }

        /// <summary>
        /// Returns an array.
        /// Case '0' - char is outside brackets, case '1' - in first level of brackets...
        /// </summary>
        internal static int[] BracketsLevel(string input)
        {
            var bracketsLevel = new int[input.Length];

            if (input.StartsWith(CalculationConstants.OpeningBracket))
            {
                bracketsLevel[0] = 1;
            }
            else if (input.StartsWith(CalculationConstants.ClosingBracket))
            {
                bracketsLevel[0] = -1;
            }

            for (var index = 1; index < input.Length; index++)
            {
                if (input[index] == CalculationConstants.OpeningBracket)
                {
                    bracketsLevel[index] = bracketsLevel[index - 1] + 1;
                }
                else if (input[index] == CalculationConstants.ClosingBracket)
                {
                    bracketsLevel[index] = bracketsLevel[index - 1] - 1;
                }
                else
                {
                    bracketsLevel[index] = bracketsLevel[index - 1];
                }
            }

            if (bracketsLevel.Last() > 0)
            {
                throw new CheckingMissedElementException
                    (element: CalculationConstants.ClosingBracket, number: bracketsLevel.Last());
            }
            else if (bracketsLevel.Last() < 0)
            {
                throw new CheckingMissedElementException
                    (element: CalculationConstants.OpeningBracket, number: Math.Abs(bracketsLevel.Last()));
            }

            return bracketsLevel;
        }
    }
}
