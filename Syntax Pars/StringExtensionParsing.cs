using System;
using System.Collections.Generic;
using System.Linq;

namespace Syntax_Pars
{
    public static partial class StringExtension
    {
        const string ValidatedFigures = "0123456789+-*/(),";
        const string PlusMinMultDiv = "+-*/";
        const string PlusMinMultDivCom = "+-*/,";
        const char Plus = '+';
        const char Minus = '-';
        const char Multiply = '*';
        const char Divide = '/';
        const char OpeningBracket = '(';
        const char ClosingBracket = ')';
        const char Comma = ',';

        public static void CheckOnFigures(this string input)
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

        public static string ValidatedUnaryMinusString(this string input)
        {
            if (input.StartsWith(Minus) || input.StartsWith(Plus))
            {
                input = "0" + input;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if ((input[index] == Minus && input[index - 1] == OpeningBracket) ||
                    (input[index] == Plus && input[index - 1] == OpeningBracket))
                {
                    input = input.Insert(index, "0");
                }
            }
            return input;
        }

        public static string CheckOnBrackets(this string input)
        {
            for (int index = 0; index < input.Length; index++)
            {
                switch (input[index])
                {
                    case OpeningBracket:
                        if (index > 0 && !"(+-*/".Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment: {input[index - 1]}{input[index]}");
                        }
                        else if (index > 0 && "),".Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment: {input[index - 1]}{input[index]}");
                        }
                        else if (index == 1 && "*/,".Contains(input.First()))
                        {
                            throw new ParsingException($"Invalid first element {input.First()}");
                        }
                        else if (input.Length == 1)
                        {
                            throw new ParsingException("Just an opening bracket?");
                        }
                        else if (index == input.Length - 1)
                        {
                            throw new ParsingException("Invalid last element '('");
                        }
                        break;
                    case ClosingBracket:
                        if (index > 0 && "(,+-*/".Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment: {input[index - 1]}{input[index]}");
                        }
                        else if (index != input.Length - 1 && !"+-*/)".Contains(input[index + 1]))
                        {
                            throw new ParsingException($"Invalid fragment: {input[index]}{input[index + 1]}");
                        }
                        else if (input.Length == 1)
                        {
                            throw new ParsingException("Just an closing bracket?");
                        }
                        else if (index == 0)
                        {
                            throw new ParsingException("Invalid first element ')'");
                        }
                        break;
                }
            }
            return input;
        }

        public static void CheckOnOperations(this string input)
        {
            if (input.Length == 1 && PlusMinMultDiv.Contains(input))
            {
                throw new ParsingException($"Just a '{input}'?");
            }
            else
            {
                for (int index = 1; index < input.Length; index++)
                {
                    if (PlusMinMultDiv.Contains(input[index]))
                    {
                        if (index == input.Length - 1)
                        {
                            throw new ParsingException($"Invalid last element {input[index]}");
                        }
                        else if (PlusMinMultDivCom.Contains(input[index - 1]))
                        {
                            throw new ParsingException($"Invalid fragment: {input[index - 1]}{input[index]}");
                        }
                        else if (PlusMinMultDivCom.Contains(input[index + 1]))
                        {
                            throw new ParsingException($"Invalid fragment: {input[index]}{input[index + 1]}");
                        }
                    }
                }
            }
        }

        public static void CheckOnComma(this string input)
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
                    else if ("(+-*/)".Contains(input[index - 1]))
                    {
                        throw new ParsingException("Wrong figure before comma");
                    }
                    for (int secondIndex = index + 1; secondIndex < input.Length; secondIndex++)
                    {
                        if (input[secondIndex] == Comma)
                        {
                            string editedInput = input.Substring(index + 1, secondIndex - index - 1);
                            if (!editedInput.Any(character => PlusMinMultDiv.Contains(character)))
                            {
                                throw new ParsingException($"Double comma: {Comma + editedInput + Comma}");
                            }
                        }
                    }
                }
            }
        }

        public static int[] BracketsLevel(string input)
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
                throw new ParsingException("Missed closing bracket?");
            }
            else if (marker.Last() < 0)
            {
                throw new ParsingException("Missed opening bracket?");
            }
            return marker;
        }

        public static string TrimBrackets(string input)
        {
            if (input.StartsWith(OpeningBracket) && input.EndsWith(ClosingBracket))
            {
                int[] marker = BracketsLevel(input: input);
                if (marker.Length == 2)
                {
                    throw new ParsingException("Empty brackets");
                }
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
                input = TrimBrackets(input: input);
            }
            return input;
        }
    }
}
