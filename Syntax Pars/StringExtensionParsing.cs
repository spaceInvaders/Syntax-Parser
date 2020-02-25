using System;
using System.Collections.Generic;
using System.Linq;

namespace Syntax_Pars
{
    public static partial class StringExtension
    {
        public static string ValidatedUnaryMinusString(this string input)
        {
            if (input.StartsWith('-') || input.StartsWith('+'))
            {
                input = "0" + input;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if ((input[index] == '-' && input[index - 1] == '(') ||
                    (input[index] == '+' && input[index - 1] == '('))
                {
                    input = input.Insert(index, "0");
                }
            }
            return input;
        }

        public static void CheckOnBrackets(this string input)
        {
            int[] marker = BracketsLevel(input: input);
            if (marker[marker.Length - 1] != 0)
            {
                throw new ParsingException("x");
            }
            else
            {
                for (int index = 0; index < input.Length; index++)
                {
                    switch (input[index])
                    {
                        case '(':
                            if (index > 0 && !"(+-*/".Contains(input[index - 1]))
                            {
                                throw new ParsingException("x");
                            }
                            if ("),*/".Contains(input[index - 1]))
                            {
                                throw new ParsingException("x");
                            }
                            break;
                        case ')':
                            if ("(,+-*/".Contains(input[index - 1]))
                            {
                                throw new ParsingException("x");
                            }
                            if (index != input.Length - 1 && "+-*/)".Contains(input[index + 1]))
                            {
                                    throw new ParsingException("x");
                            }
                            break;
                    }
                }
            }
        }

        public static void CheckOnFigures(this string input)
        {
            if (!input.All(character => "0123456789+-*/(),".Contains(character)))
            {
                var exceptionElements = input.ToCharArray().Except("0123456789+-*/()".ToCharArray());
                List<char> elements = new List<char>();
                foreach (char character in exceptionElements)
                {
                    elements.Add(character);
                }
                string wrongFigures = new string(elements.ToArray());
                throw new ParsingException($"Wrong figures: {wrongFigures}");
            }
        }

        public static void CheckOnOperations(this string input)
        {
            if (input.Length == 1 && "+-*/".Contains(input))
            {
                throw new ParsingException("x");
            }
            else
            {
                for (int index = 1; index < input.Length; index++)
                {
                    if ("+-*/".Contains(input[index]))
                    {
                        if (index == input.Length - 1)
                        {
                            throw new ParsingException("x");
                        }
                        else if ("+-*/,".Contains(input[index - 1]))
                        {
                            throw new ParsingException("x");
                        }
                        else if ("+-*/,".Contains(input[index + 1]))
                        {
                            throw new ParsingException("x");
                        }
                    }
                }
            }
        }

        public static void CheckOnComma(this string input)
        {
            input = TrimBrackets(input: input);
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == ',')
                {
                    if (index == 0)
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
                        if (input[secondIndex] == ',')
                        {
                            string editedInput = input.Substring(index + 1, secondIndex - index - 1);
                            if (!editedInput.Any(character => "+-*/".Contains(character)))
                            {
                                throw new ParsingException($"Double comma: {"," + editedInput + ","}");
                            }
                        }
                    }
                }
            }
        }

        public static int[] BracketsLevel(string input)
        {
            int[] marker = new int[input.Length];
            if (input.StartsWith('('))
            {
                marker[0] = 1;
            }
            if (input.StartsWith(')'))
            {
                marker[0] = -1;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if (input[index] == '(')
                {
                    marker[index] = marker[index - 1] + 1;
                }
                else if (input[index] == ')')
                {
                    marker[index] = marker[index - 1] - 1;
                }
                else
                {
                    marker[index] = marker[index - 1];
                }
            }
            return marker;
        }

        public static string TrimBrackets(string input)
        {
            if (input.StartsWith('(') && input.EndsWith(')'))
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
            if (input.StartsWith('(') && input.EndsWith(')'))
            {
                input = TrimBrackets(input: input);
            }
            return input;
        }
    }
}
