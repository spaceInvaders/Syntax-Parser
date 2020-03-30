using System;
using System.Linq;

namespace Syntax_Pars
{
    internal static class BracketsHelper
    {
        internal static string TrimBrackets(string input)
        {
            if (input.StartsWith(CalculationConstants.OpeningBracket) &&
                input.EndsWith(CalculationConstants.ClosingBracket))
            {
                var bracketsLevel = BracketsLevel(input: input);

                for (int index = 1; index < input.Length - 1; index++)
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

        internal static int[] BracketsLevel(string input)
        {
            var bracketsLevel = new int[input.Length];

            if (input.StartsWith(CalculationConstants.OpeningBracket))
            {
                bracketsLevel[0] = 1;
            }
            //*** check via tests if code block bellow is needed till now
            else if (input.StartsWith(CalculationConstants.ClosingBracket))
            {
                bracketsLevel[0] = -1;
            }
            //***
    
            for (int index = 1; index < input.Length; index++)
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
                throw new ParsingMissedElementException(element: CalculationConstants.ClosingBracket, number: bracketsLevel.Last());
            }
            else if (bracketsLevel.Last() < 0)
            {
                throw new ParsingMissedElementException(element: CalculationConstants.OpeningBracket, number: Math.Abs(bracketsLevel.Last()));
            }

            return bracketsLevel;
        }
    }
}
