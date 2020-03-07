using System.Collections.Generic;
using System.Linq;

namespace Syntax_Pars
{
    static partial class StringExtension
    {
        const string ValidatedFigures = "0123456789+-*/^(),";
        const string PlusMinMultDivPowBrackets = "+-*/^)(";
        const string PlusMinMultDivPow = "+-*/^";
        const string PlusMinMultDivPowCom = "+-*/^,";
        const char Zero = '0';
        const char Plus = '+';
        const char Minus = '-';
        const char Multiply = '*';
        const char Divide = '/';
        const char Power = '^';
        const char OpeningBracket = '(';
        const char ClosingBracket = ')';
        const char Comma = ',';

        internal static int FindLastOerationWithPriorityPlusMinus(this string input)
        {
            int[] bracketsLevel = BracketsLevel(input: input);
            int lastOperationIndex = 0;
            bool multDivOperationHasBeenFound = false;
            bool powerOperationHasBeenFound = false;
            for (int index = input.Length - 1; index >= 0; index--)
            {
                if ((input[index] == Plus || input[index] == Minus) && bracketsLevel[index] == 0)
                {
                    lastOperationIndex = index;
                    break;
                }
                else if ((input[index] == Multiply || input[index] == Divide) && bracketsLevel[index] == 0 && !multDivOperationHasBeenFound)
                {
                    lastOperationIndex = index;
                    multDivOperationHasBeenFound = true;
                }
                else if (input[index] == Power && bracketsLevel[index] == 0 && !multDivOperationHasBeenFound && !powerOperationHasBeenFound)
                {
                    lastOperationIndex = index;
                    powerOperationHasBeenFound = true;
                }
            }
            return lastOperationIndex;
        }

        internal static void CheckOnFigures(this string input)
        {
            if (!input.All(character => ValidatedFigures.Contains(character)))
            {
                var exceptionElements = input.ToCharArray().Except(ValidatedFigures.ToCharArray());
                List<char> elements = new List<char>();
                foreach (char character in exceptionElements)
                {
                    elements.Add(character);
                }
                string invalidFigures = new string(elements.ToArray());
                throw new ParsingException($"Invalid figures: {invalidFigures}");
            }
        }

        internal static string ValidatedUnaryMinusString(this string input)
        {
            if (input.StartsWith(Minus) || input.StartsWith(Plus))
            {
                input = Zero + input;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if ((input[index] == Minus && input[index - 1] == OpeningBracket) ||
                    (input[index] == Plus && input[index - 1] == OpeningBracket))
                {
                    input = input.Insert(index, Zero.ToString());
                }
            }
            return input;
        }

        internal static string CheckOnBrackets(this string input)
        {
            int[] bracketsLevel = StringExtension.BracketsLevel(input: input);
            if (bracketsLevel.Last() == 0 && input.All(character => "()".Contains(character)))
            {
                throw new ParsingException("Empty brackets");
            }
            for (int index = 0; index < input.Length; index++)
            {
                switch (input[index])
                {
                    case OpeningBracket:
                        if (index > 0 && !"(+-*/^".Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                        }
                        else if (index == input.Length - 1)
                        {
                            throw new ParsingException($"Invalid last element {OpeningBracket}");
                        }
                        else if ("*/,^".Contains(input[index + 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index]}{input[index + 1]}'");
                        }
                        break;
                    case ClosingBracket:
                        if (index > 0 && "(,+-*/^".Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                        }
                        else if (index != input.Length - 1 && !"+-*/^)".Contains(input[index + 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index]}{input[index + 1]}'");
                        }
                        break;
                }
            }
            return input;
        }

        internal static void CheckOnOperations(this string input)
        {
            if (input.Length == 1 && PlusMinMultDivPow.Contains(input))
            {
                throw new ParsingException($"Just a '{input}'?");
            }
            else if ("*/^".Contains(input.First()))
            {
                throw new ParsingException($"Invalid first element '{input.First()}'");
            }
            else
            {
                for (int index = 1; index < input.Length; index++)
                {
                    if (PlusMinMultDivPow.Contains(input[index]))
                    {
                        if (index == input.Length - 1)
                        {
                            throw new ParsingException($"Invalid last element '{input[index]}'");
                        }
                        else if (PlusMinMultDivPowCom.Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                        }
                        else if (PlusMinMultDivPowCom.Contains(input[index + 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index]}{input[index + 1]}'");
                        }
                    }
                }
            }
        }

        internal static void CheckOnComma(this string input)
        {
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == Comma)
                {
                    if (input.Length == 1)
                    {
                        throw new ParsingException("Just a comma?");
                    }
                    else if (index == 0)
                    {
                        throw new ParsingException("Comma at the beginning");
                    }
                    else if (index == input.Length - 1)
                    {
                        throw new ParsingException("Comma at the end");
                    }
                    else if (PlusMinMultDivPowBrackets.Contains(input[index - 1]))
                    {
                        throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                    }
                    for (int secondIndex = index + 1; secondIndex < input.Length; secondIndex++)
                    {
                        if (input[secondIndex] == Comma)
                        {
                            string editedInput = input.Substring(index + 1, secondIndex - index - 1);
                            if (!editedInput.Any(character => PlusMinMultDivPow.Contains(character)))
                            {
                                throw new ParsingException($"Double comma '{Comma + editedInput + Comma}'");
                            }
                        }
                    }
                }
            }
        }

        internal static string TrimExcessiveZerosString(this string input)
        {
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == Comma)
                {
                    for (int secondIndex = index + 1; secondIndex < input.Length; secondIndex++)
                    {
                        if ("+-/*)".Contains(input[secondIndex]))
                        {
                            while (input[secondIndex - 1] == Zero && !PlusMinMultDivPowBrackets.Contains(input[secondIndex - 2]))
                            {
                                if (input[secondIndex - 2] == Comma)
                                {
                                    input = input.Substring(0, secondIndex - 2) + input.Substring(secondIndex, input.Length - secondIndex);
                                    return input;
                                }
                                input = input.Substring(0, secondIndex - 1) + input.Substring(secondIndex, input.Length - secondIndex);
                                secondIndex -= 1;
                            }
                            return input;
                        }
                        else if (secondIndex == input.Length - 1)
                        {
                            while (input[secondIndex - 1] == Zero)
                            {
                                input = input.Substring(0, secondIndex - 1) + input.Substring(secondIndex, input.Length - secondIndex);
                                secondIndex -= 1;
                                if (input[secondIndex - 1] == Comma)
                                {
                                    input = input.Substring(0, secondIndex - 1);
                                    break;
                                }
                                else if ("123456789".Contains(input[secondIndex - 1]))
                                {
                                    input = input.Substring(0, secondIndex);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return input;
        }

        internal static int[] BracketsLevel(string input)
        {
            int[] bracketsLevel = new int[input.Length];
            if (input.StartsWith(OpeningBracket))
            {
                bracketsLevel[0] = 1;
            }
            if (input.StartsWith(ClosingBracket))
            {
                bracketsLevel[0] = -1;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if (input[index] == OpeningBracket)
                {
                    bracketsLevel[index] = bracketsLevel[index - 1] + 1;
                }
                else if (input[index] == ClosingBracket)
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
                throw new ParsingException($"Missed {bracketsLevel.Last()} closing bracket(s)?");
            }
            else if (bracketsLevel.Last() < 0)
            {
                throw new ParsingException($"Missed {bracketsLevel.Last() * (-1)} opening bracket(s)?");
            }
            return bracketsLevel;
        }

        internal static string TrimBracketsString(this string input)
        {
            if (input.StartsWith(OpeningBracket) && input.EndsWith(ClosingBracket))
            {
                int[] bracketsLevel = BracketsLevel(input: input);
                for (int index = 1; index < input.Length - 1; index++)
                {
                    if (bracketsLevel[index] == 0)
                    {
                        return input;
                    }
                    else if (index == input.Length - 2)
                    {
                        input = input.Substring(1, input.Length - 2);
                    }
                }
            }
            if (input.StartsWith(OpeningBracket) && input.EndsWith(ClosingBracket))
            {
                input = input.TrimBracketsString();
            }
            return input;
        }
    }
}


