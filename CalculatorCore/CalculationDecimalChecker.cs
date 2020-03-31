using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;

namespace CalculatorCore
{
    static class CalculationDecimalChecker
    {
        internal static string CheckInput(string input, CultureInfo culture)
        {
            var editedInput = RemoveWhiteSpaces(input: input);
            editedInput = ParseInput(input: editedInput, culture: culture);
            editedInput = RemoveGroupSeparator(input: editedInput, culture: culture);
            editedInput = BracketsHelper.TrimBrackets(input: editedInput);
            editedInput = editedInput.Replace(CalculationConstants.PiChar.ToString(), Math.PI.ToString(culture));

            return editedInput;
        }

        #region PrivateMethods

        private static char Separator(CultureInfo culture) => Convert.ToChar(culture.NumberFormat.NumberDecimalSeparator);

        private static char GroupSeparator(CultureInfo culture) => Convert.ToChar(culture.NumberFormat.NumberGroupSeparator);

        private static string ValidCharacters(CultureInfo culture) => "0123456789+-*/^)(p" + Separator(culture: culture);

        private static string PlusMinMultDivPowSep(CultureInfo culture) => "+-*/^" + Separator(culture: culture);

        private static string RemoveWhiteSpaces(string input)
        {
            return new string(input.ToCharArray().Where(character => !Char.IsWhiteSpace(character)).ToArray());
        }

        private static string RemoveGroupSeparator(string input, CultureInfo culture)
        {
            if (Separator(culture: culture) == CalculationConstants.Comma &&
                Convert.ToChar(GroupSeparator(culture: culture)) == CalculationConstants.Dot)

                input = input.Replace(CalculationConstants.Dot.ToString(), String.Empty);

            else if (Separator(culture: culture) == CalculationConstants.Dot
                && Convert.ToChar(GroupSeparator(culture: culture)) == CalculationConstants.Comma)

                input = input.Replace(CalculationConstants.Comma.ToString(), String.Empty);

            return input;
        }

        private static string ParseInput(string input, CultureInfo culture)
        {
            string editedInput = input;

            for (int parseIndex = 0; parseIndex < input.Length; parseIndex++)
            {
                int editedInputParseIndex = parseIndex - (input.Length - editedInput.Length);

                switch (input[parseIndex])
                {
                    case CalculationConstants.OpeningBracket:
                    case CalculationConstants.ClosingBracket:
                        CheckOnBrackets(input: input, index: parseIndex, bracket: input[parseIndex], culture: culture);
                        break;
                    case CalculationConstants.Minus:
                    case CalculationConstants.Plus:
                        if (parseIndex == 0 || input[parseIndex - 1] == CalculationConstants.OpeningBracket)
                            editedInput = ValidatedUnaryMinus(input: editedInput, index: editedInputParseIndex);
                        else
                            CheckOnOperations(input: input, index: parseIndex, culture: culture);
                        break;
                    case CalculationConstants.Multiply:
                    case CalculationConstants.Divide:
                    case CalculationConstants.Power:
                        CheckOnOperations(input: input, index: parseIndex, culture: culture);
                        break;
                    case CalculationConstants.PiChar:
                        CheckOnPI(input: input, index: parseIndex);
                        break;
                    default:
                        if (input[parseIndex] == Separator(culture: culture) || input[parseIndex] == GroupSeparator(culture: culture))
                            CheckOnSeparator(input: editedInput, index: editedInputParseIndex, culture: culture);
                        else
                            CheckOnValidatedFigures(input: input, index: parseIndex, culture: culture);
                        break;
                }
            }

            return editedInput;
        }

        #region PrivateParseInputMethods

        private static void CheckOnValidatedFigures(string input, int index, CultureInfo culture)
        {
            if (!Char.IsDigit(input[index]))
            {
                string inputForCheck = input[index..];

                if (!inputForCheck.All(character => ValidCharacters(culture: culture).Contains(character)))
                {
                    var exceptionElements = inputForCheck.ToCharArray().Except(ValidCharacters(culture: culture).ToCharArray());
                    var elements = new List<char>();

                    foreach (char character in exceptionElements)
                    {
                        elements.Add(character);
                    }

                    var invalidFigures = new string(elements.ToArray());

                    throw new ParsingInvalidElemenstException(invalidElements: invalidFigures);

                }
            }
        }

        private static string ValidatedUnaryMinus(string input, int index)
        {
            if (input.Length == 1)

                throw new ParsingJustAnElementException(input: input[index].ToString());

            else if (input.StartsWith(CalculationConstants.Minus) || input.StartsWith(CalculationConstants.Plus))

                input = CalculationConstants.Zero + input;

            else if ((input[index] == CalculationConstants.Minus &&
                input[index - 1] == CalculationConstants.OpeningBracket) ||
                (input[index] == CalculationConstants.Plus && input[index - 1] == CalculationConstants.OpeningBracket))

                input = input.Insert(index, CalculationConstants.Zero.ToString());

            return input;
        }

        private static void CheckOnBrackets(string input, int index, char bracket, CultureInfo culture)
        {
            var bracketsLevel = BracketsHelper.BracketsLevel(input: input);

            if (bracketsLevel.Last() == 0 && input.All(character => "()".Contains(character)))
            {
                //event
                throw new ParsingInvalidFragmentException(fragment: input);
            }

            switch (bracket)
            {
                case CalculationConstants.OpeningBracket:

                    if (index > 0 && !CalculationConstants.PlusMinMultDivPowOpenBrack.Contains(input[index - 1]))
                    {
                        throw new ParsingInvalidFragmentException
                            (fragment: input[index - 1].ToString() + input[index], firstEntry: index - 1, lastEntry: index);
                    }
                    else if (index == input.Length - 1)
                    {
                        throw new ParsingInvalidLastElementException
                            (element: CalculationConstants.OpeningBracket, location: index);
                    }
                    else if (("*/^" + Separator(culture: culture).ToString()).Contains(input[index + 1]))
                    {
                        throw new ParsingInvalidFragmentException
                            (fragment: input[index].ToString() + input[index + 1], firstEntry: index, lastEntry: index + 1);
                    }
                    break;

                case CalculationConstants.ClosingBracket:

                    if (index > 0 && ("(+-*/^" + Separator(culture: culture).ToString()).Contains(input[index - 1]))
                    {
                        throw new ParsingInvalidFragmentException
                            (fragment: input[index - 1].ToString() + input[index], firstEntry: index - 1, lastEntry: index);
                    }
                    else if (index != input.Length - 1 &&
                            !CalculationConstants.PlusMinMultDivPowClosBrack.Contains(input[index + 1]))
                         {
                            throw new ParsingInvalidFragmentException
                            (fragment: input[index].ToString() + input[index + 1], firstEntry: index, lastEntry: index + 1);
                         }
                    break;
            }
        }

        private static void CheckOnOperations(string input, int index, CultureInfo culture)
        {
            if (input.Length == 1)
            {
                throw new ParsingJustAnElementException(input: input);
            }
            else if ("*/^".Contains(input.First()))
            {
                throw new ParsingInvalidFirstElementException(element: input.First());
            }
            else if (index == input.Length - 1)
            {
                throw new ParsingInvalidLastElementException(element: input[index], location: index);
            }
            else if (PlusMinMultDivPowSep(culture: culture).Contains(input[index - 1]))
            {
                throw new ParsingInvalidFragmentException
                    (fragment: input[index - 1].ToString() + input[index], firstEntry: index - 1, lastEntry: index);
            }
            else if (PlusMinMultDivPowSep(culture: culture).Contains(input[index + 1]))
            {
                throw new ParsingInvalidFragmentException
                    (fragment: input[index].ToString() + input[index + 1], firstEntry: index, lastEntry: index + 1);
            }
        }

        private static void CheckOnSeparator(string input, int index, CultureInfo culture)
        {
            if (input.Length == 1)
            {
                throw new ParsingJustAnElementException(input: input);
            }
            else if (index == 0)
            {
                throw new ParsingInvalidFirstElementException(element: input[index]);
            }
            else if (index == input.Length - 1)
            {
                throw new ParsingInvalidLastElementException(element: input[index], location: index);
            }
            else if (CalculationConstants.PlusMinMultDivPowBrackets.Contains(input[index - 1]))
            {
                throw new ParsingInvalidFragmentException
                    (fragment: input[index - 1].ToString() + input[index], firstEntry: index - 1, lastEntry: index);
            }

            for (int secondIndex = index + 1; secondIndex < input.Length; secondIndex++)
            {
                if (input[index] == Separator(culture: culture) && input[secondIndex] == Separator(culture: culture))
                {
                    var editedInput = input.Substring(index + 1, secondIndex - index - 1);
                    if (!editedInput.Any(character => CalculationConstants.PlusMinMultDivPow.Contains(character)))

                        throw new ParsingInvalidFragmentException
                            (fragment: Separator(culture: culture) + editedInput + Separator(culture: culture),
                            firstEntry: index, lastEntry: secondIndex);
                }
                if (((input[index] == GroupSeparator(culture: culture) || input[index] == Separator(culture: culture))
                        && input[secondIndex] == GroupSeparator(culture: culture)) && (secondIndex == index + 1))
                {
                        throw new ParsingInvalidFragmentException
                        (fragment: input[index].ToString() + input[index + 1], firstEntry: index, lastEntry: secondIndex);
                }
            }
        }

        private static void CheckOnPI(string input, int index)
        {
            if (index > 0 && !CalculationConstants.PlusMinMultDivPowOpenBrack.Contains(input[index - 1]))
            {
                throw new ParsingInvalidFragmentException
                    (fragment: input[index - 1].ToString() + input[index], firstEntry: index - 1, lastEntry: index);
            }
            else if (index != input.Length - 1 && !CalculationConstants.PlusMinMultDivPowClosBrack.Contains(input[index + 1]))
            {
                throw new ParsingInvalidFragmentException
                    (fragment: input[index].ToString() + input[index + 1], firstEntry: index, lastEntry: index + 1);
            }
        }

        #endregion

        #endregion

    }
}


