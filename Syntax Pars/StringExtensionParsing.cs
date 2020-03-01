using System.Collections.Generic;
using System.Linq;

namespace Syntax_Pars
{
    static partial class StringExtension
    {
        const string ValidatedFigures = "0123456789+-*/(),";
        const string PlusMinMultDivBrackets = "+-*/)(";
        const string PlusMinMultDiv = "+-*/";
        const string PlusMinMultDivCom = "+-*/,";
        const char Zero = '0';
        const char Plus = '+';
        const char Minus = '-';
        const char Multiply = '*';
        const char Divide = '/';
        const char OpeningBracket = '(';
        const char ClosingBracket = ')';
        const char Comma = ',';

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
            int[] marker = BracketsLevel(input: input);
            if (marker.Last() == 0 && input.All(character => "()".Contains(character)))
            {
                throw new ParsingException("Empty brackets");
            }
            for (int index = 0; index < input.Length; index++)
            {
                switch (input[index])
                {
                    case OpeningBracket:
                        if (index > 0 && !"(+-*/".Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                        }
                        else if (index == input.Length - 1)
                        {
                            throw new ParsingException($"Invalid last element {OpeningBracket}");
                        }
                        else if ("*/,".Contains(input[index + 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index]}{input[index + 1]}'");
                        }
                        break;
                    case ClosingBracket:
                        if (index > 0 && "(,+-*/".Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                        }
                        else if (index != input.Length - 1 && !"+-*/)".Contains(input[index + 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index]}{input[index + 1]}'");
                        }
                        else if (index == 0)
                        {
                            throw new ParsingException($"Invalid first element {ClosingBracket}");
                        }
                        break;
                }
            }
            return input;
        }

        internal static void CheckOnOperations(this string input)
        {
            if (input.Length == 1 && PlusMinMultDiv.Contains(input))
            {
                throw new ParsingException($"Just a '{input}'?");
            }
            else if (input.First() == Multiply || input.First() == Divide)
            {
                throw new ParsingException($"Invalid first element '{input.First()}'");
            }
            else
            {
                for (int index = 1; index < input.Length; index++)
                {
                    if (PlusMinMultDiv.Contains(input[index]))
                    {
                        if (index == input.Length - 1)
                        {
                            throw new ParsingException($"Invalid last element '{input[index]}'");
                        }
                        else if (PlusMinMultDivCom.Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                        }
                        else if (PlusMinMultDivCom.Contains(input[index + 1]))
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
                    else if (PlusMinMultDivBrackets.Contains(input[index - 1]))
                    {
                        throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
                    }
                    for (int secondIndex = index + 1; secondIndex < input.Length; secondIndex++)
                    {
                        if (input[secondIndex] == Comma)
                        {
                            string editedInput = input.Substring(index + 1, secondIndex - index - 1);
                            if (!editedInput.Any(character => PlusMinMultDiv.Contains(character)))
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
                            while (input[secondIndex - 1] == Zero && !PlusMinMultDivBrackets.Contains(input[secondIndex - 2]))
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
            int[] marker = new int[input.Length];
            if (input.StartsWith(OpeningBracket))
            {
                marker[0] = 1;
            }
            if (input.StartsWith(ClosingBracket))
            {
                marker[0] = -1;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if (input[index] == OpeningBracket)
                {
                    marker[index] = marker[index - 1] + 1;
                }
                else if (input[index] == ClosingBracket)
                {
                    marker[index] = marker[index - 1] - 1;
                }
                else
                {
                    marker[index] = marker[index - 1];
                }
            }
            if (marker.Last() > 0)
            {
                throw new ParsingException($"Missed {marker.Last()} сlosing bracket/s?");
            }
            else if (marker.Last() < 0)
            {
                throw new ParsingException($"Missed {marker.Last()*(-1)} opening bracket/s?");
            }
            return marker;
        }

        internal static string TrimBracketsString(this string input)
        {
            if (input.StartsWith(OpeningBracket) && input.EndsWith(ClosingBracket))
            {
                int[] marker = BracketsLevel(input: input);
                for (int index = 1; index < input.Length - 1; index++)
                {
                    if (marker[index] == 0)
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
