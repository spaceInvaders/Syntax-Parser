using System.Collections.Generic;
using System.Linq;

namespace Syntax_Pars
{
    static partial class StringExtension
    {
        const string ValidatedFigures = "0123456789+-*/^(),";
        const string Digits = "0123456789";
        const string PlusMinMultDivPowBrackets = "+-*/^)(";
        const string PlusMinMultDivPow = "+-*/^";
        const string PlusMinMultDivPowSep = "+-*/^,";
        const char Zero = '0';
        const char Plus = '+';
        const char Minus = '-';
        const char Multiply = '*';
        const char Divide = '/';
        const char Power = '^';
        const char OpeningBracket = '(';
        const char ClosingBracket = ')';
        const char Separator = ',';

        internal static string ParseInputString(this string input)
        {
            string editedInput = input;
            for (int parseIndex = 0; parseIndex < input.Length; parseIndex++)
            {
                int editedInputParseIndex = parseIndex - (input.Length - editedInput.Length);
                switch (input[parseIndex])
                {
                    case OpeningBracket:
                        CheckOnBrackets(input: input, index: parseIndex, bracket: OpeningBracket);
                        break;
                    case ClosingBracket:
                        CheckOnBrackets(input: input, index: parseIndex, bracket: ClosingBracket);
                        break;
                    case Separator:
                        CheckOnSeparator(input: editedInput, index: editedInputParseIndex);
                        editedInput = editedInput.TrimExcessiveZerosString(index: editedInputParseIndex);
                        break;
                    case Plus:
                        if (parseIndex == 0 || input[parseIndex - 1] == OpeningBracket)
                            editedInput = editedInput.ValidatedUnaryMinusString(index: editedInputParseIndex);
                        else
                            goto case Multiply;
                        break;
                    case Minus:
                        goto case Plus;
                    case Multiply:
                        CheckOnOperations(input: input, index: parseIndex);
                        break;
                    case Divide:
                        goto case Multiply;
                    case Power:
                        goto case Multiply;
                    default:
                        CheckOnValidatedFigures(input: input, index: parseIndex);
                        break;
                }
            }
            return editedInput;
        }

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

        internal static void CheckOnValidatedFigures(string input, int index)
        {
            if (!Digits.Contains(input[index]))
            {
                string inputForCheck = input[index..];
                if (!inputForCheck.All(character => ValidatedFigures.Contains(character)))
                {
                    var exceptionElements = inputForCheck.ToCharArray().Except(ValidatedFigures.ToCharArray());
                    List<char> elements = new List<char>();
                    foreach (char character in exceptionElements)
                    {
                        elements.Add(character);
                    }
                    string invalidFigures = new string(elements.ToArray());
                    throw new ParsingException($"Invalid figures: {invalidFigures}");
                }
            }
        }

        internal static string ValidatedUnaryMinusString(this string input, int index)
        {
            if (input.StartsWith(Minus) || input.StartsWith(Plus))
            {
                input = Zero + input;
            }
            else if ((input[index] == Minus && input[index - 1] == OpeningBracket) ||
                    (input[index] == Plus && input[index - 1] == OpeningBracket))
                 {
                    input = input.Insert(index, Zero.ToString());
                 }
            return input;
        }

        internal static void CheckOnBrackets(string input, int index, char bracket)
        {
            int[] bracketsLevel = StringExtension.BracketsLevel(input: input);
            if (bracketsLevel.Last() == 0 && input.All(character => "()".Contains(character)))
            {
                throw new ParsingException("Empty brackets");
            }
            switch (bracket)
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

        internal static void CheckOnOperations(string input, int index)
        {
            if (input.Length == 1)
            {
                throw new ParsingException($"Just a '{input}'?");
            }
            else if ("*/^".Contains(input.First()))
            {
                throw new ParsingException($"Invalid first element '{input.First()}'");
            }
            else if (index == input.Length - 1)
            {
                throw new ParsingException($"Invalid last element '{input[index]}'");
            }
            else if (PlusMinMultDivPowSep.Contains(input[index - 1]))
            {
                throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
            }
            else if (PlusMinMultDivPowSep.Contains(input[index + 1]))
            {
                throw new ParsingException($"Invalid fragment '{input[index]}{input[index + 1]}'");
            }
        }

        internal static void CheckOnSeparator(this string input, int index)
        {
            if (input.Length == 1)
            {
                throw new ParsingException("Just a separator?");
            }
            else if (index == 0)
            {
                throw new ParsingException("Separator at the beginning");
            }
            else if (index == input.Length - 1)
            {
                throw new ParsingException("Separator at the end");
            }
            else if (PlusMinMultDivPowBrackets.Contains(input[index - 1]))
            {
                throw new ParsingException($"Invalid fragment '{input[index - 1]}{input[index]}'");
            }
            for (int afterSeparator = index + 1; afterSeparator < input.Length; afterSeparator++)
            {
                if (input[afterSeparator] == Separator)
                {
                    string editedInput = input.Substring(index + 1, afterSeparator - index - 1);
                    if (!editedInput.Any(character => PlusMinMultDivPow.Contains(character)))
                    {
                        throw new ParsingException($"Double separator '{Separator + editedInput + Separator}'");
                    }
                }
            }
        }

        internal static string TrimExcessiveZerosString(this string input, int index)
        {
            for (int afterSeparator = index + 1; afterSeparator < input.Length; afterSeparator++)
            {
                if ("+-/*)".Contains(input[afterSeparator]))
                {
                    while (input[afterSeparator - 1] == Zero && !PlusMinMultDivPowBrackets.Contains(input[afterSeparator - 2]))
                    {
                        if (input[afterSeparator - 2] == Separator)
                        {
                            input = input.Substring(0, afterSeparator - 2) + input[afterSeparator..];
                            break;
                        }
                        input = input.Substring(0, afterSeparator - 1) + input[afterSeparator..];
                        afterSeparator -= 1;
                    }
                    break;
                }
                else if (afterSeparator == input.Length - 1)
                {
                    while (input[afterSeparator] == Zero)
                    {
                        input = input.Substring(0, afterSeparator - 1) + input[afterSeparator..];
                        afterSeparator -= 1;
                        if (input[afterSeparator - 1] == Separator)
                        {
                            input = input.Substring(0, afterSeparator - 1);
                            break;
                        }
                        else if ("123456789".Contains(input[afterSeparator - 1]))
                        {
                            input = input.Substring(0, afterSeparator);
                            break;
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


