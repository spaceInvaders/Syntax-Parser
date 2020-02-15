using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    public static partial class StringExtensionBase
    {
        public static string CheckOnMinus(this string input)
        {
            if (input[0] == '-' || input[0] == '+')
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

        public static string CheckOnBrackets(this string input)
        {
            int[] marker = BracketsLevel(input: input);
            if (marker[marker.Length - 1] != 0)
            {
                input = null;
            }
            else
            {
                for (int index = 0; index < input.Length; index++)
                {
                    switch (input[index])
                    {
                        case '(':
                            if (index > 0)
                            {
                                if (input[index - 1] != '(' && input[index - 1] != '+' &&
                                    input[index - 1] != '-' && input[index - 1] != '*' &&
                                    input[index - 1] != '/')
                                {
                                    return null;
                                }
                            }
                            if (input[index + 1] == ')' || input[index + 1] == ',' ||
                                input[index + 1] == '*' || input[index + 1] == '/')
                            {
                                return null;
                            }
                            break;
                        case ')':
                            if (input[index - 1] == '(' || input[index - 1] == ',' ||
                                input[index - 1] == '+' || input[index - 1] == '-' ||
                                input[index - 1] == '*' || input[index - 1] == '/')
                            {
                                return null;
                            }
                            if (index != input.Length - 1)
                            {
                                if (input[index + 1] != '+' && input[index + 1] != '-' &&
                                    input[index + 1] != '*' && input[index + 1] != '/' &&
                                    input[index + 1] != ')')
                                {
                                    return null;
                                }
                            }
                            break;
                    }
                }
            }
            return input;
        }

        public static string CheckOnFigures(this string input)
        {
            for (int index = 0; index < input.Length; index++)
            {
                char myChar = input[index];
                if ((myChar != '0') && (myChar != '1') &&
                    (myChar != '2') && (myChar != '3') && 
                    (myChar != '4') && (myChar != '5') &&
                    (myChar != '6') && (myChar != '7') &&
                    (myChar != '8') && (myChar != '9') &&
                    (myChar != '+') && (myChar != '-') &&
                    (myChar != '*') && (myChar != '/') &&
                    (myChar != '(') && (myChar != ')') &&
                    (myChar != ','))
                {
                    return null;
                }
            }
            return input;
        }

        public static string CheckOnOperations(this string input)
        {
            if (input == "+" || input == "-" ||
                input == "*" || input == "/")
            {
                return null;
            }
            else
            {
                for (int index = 1; index < input.Length; index++)
                {
                    if (input[index] == '+' || input[index] == '-' ||
                        input[index] == '*' || input[index] == '/')
                    {
                        if (index == input.Length - 1)
                        {
                            return null;
                        }
                        else if (input[index - 1] == '+' || input[index - 1] == '-' ||
                                 input[index - 1] == '*' || input[index - 1] == '/' ||
                                 input[index - 1] == ',')
                             {
                                 return null;
                             }
                        else if (input[index + 1] == '+' || input[index + 1] == '-' ||
                                 input[index + 1] == '*' || input[index + 1] == '/' ||
                                 input[index + 1] == ',')
                             {
                                 return null;
                             }
                    }
                }
            }
            return input;
        }

        public static string CheckOnComma(this string input)
        {
            input = TrimBrackets(input: input);
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == ',')
                {
                    if (index == 0 || index == input.Length - 1)
                    {
                        return null;
                    }
                    else
                    {
                        if (input[index - 1] == '(' || input[index - 1] == ')' ||
                            input[index - 1] == '+' || input[index - 1] == '-' ||
                            input[index - 1] == '*' || input[index - 1] == '/')
                        {
                            return null;
                        }
                    }
                    for (int currentIndex = index + 1; currentIndex < input.Length; currentIndex++)
                    {
                        if (input[currentIndex] == ',')
                        {
                            for (int newIndex = index + 1; newIndex < currentIndex; newIndex++)
                            {
                                if (input[newIndex] == '+' || input[newIndex] == '-' ||
                                    input[newIndex] == '*' || input[newIndex] == '/')
                                {
                                    break;
                                }
                                else if (newIndex == currentIndex - 1)
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }
            }
            return input;
        }

        public static int[] BracketsLevel(string input)
        {
            int[] marker = new int[input.Length];
            if (input[0] == '(')
            {
                marker[0] = 1;
            }
            if (input[0] == ')')
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
            if (input[0] == '(' && input[input.Length - 1] == ')')
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
            if (input[0] == '(' && input[input.Length - 1] == ')')
            {
                input = TrimBrackets(input: input);
            }
            return input;
        }
    }
}
