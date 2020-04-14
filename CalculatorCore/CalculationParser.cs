using System;
using System.Globalization;
using System.Linq;

namespace CalculatorCore
{
    public static class CalculationParser
    {
        public static ICalculationOperation GrowNodeTree(string input, CultureInfo culture)
        {
            string editedInput = CalculationDecimalChecker.VerifyInput(input: input, culture: culture);
            
            return MakeNode(input: editedInput, culture: culture);
        }

        #region PrivateMethods

        private static ICalculationOperation MakeNode(string input, CultureInfo culture)
        {
            ICalculationOperation node = null;

            if (input.Any(character => CalculationConstants.PlusMinMultDivPow.Contains(character)))
            {
                node = SplitToNodes(input: input, culture: culture);
            }
            else
            {
                input = BracketsHelper.TrimBrackets(input);
                node = new Number(value: decimal.Parse(input, culture));
            }

            return node;
        }

        private static ICalculationOperation SplitToNodes(string input, CultureInfo culture)
        {
            input = BracketsHelper.TrimBrackets(input);

            /* Since people read from left to right, to ensure the correct order of operations,
             * node tree should be built from right to left with priority of operations outside brackets:
             * plus or minus, multply or divide, power */

            int lastOperationIndex = FindLastOerationWithPriorityPlusMinus(input: input);
            var rightString = input.Substring(lastOperationIndex + 1);
            var leftString = input.Substring(0, lastOperationIndex);
            var operation = input[lastOperationIndex];

            ICalculationOperation node = null;
            ICalculationOperation leftOperand = MakeNode(input: leftString, culture: culture);
            ICalculationOperation rightOperand = MakeNode(input: rightString, culture: culture);

            switch (operation)
            {
                case CalculationConstants.Plus:
                    node = new Addition(leftOperand, rightOperand);
                    break;
                case CalculationConstants.Minus:
                    node = new Subtraction(leftOperand, rightOperand);
                    break;
                case CalculationConstants.Divide:
                    node = new Division(leftOperand, rightOperand);
                    break;
                case CalculationConstants.Multiply:
                    node = new Multiplication(leftOperand, rightOperand);
                    break;
                case CalculationConstants.Power:
                    node = new ToThePower(leftOperand, rightOperand);
                    break;
            }

            return node;
        }

        private static int FindLastOerationWithPriorityPlusMinus(string input)
        {
            var bracketsLevel = BracketsHelper.BracketsLevel(input: input);
            int lastOperationIndex = 0;
            bool multDivOperationHasBeenFound = false;
            bool powerOperationHasBeenFound = false;

            for (int index = input.Length - 1; index >= 0; index--)
            {
                if ((input[index] == CalculationConstants.Plus || input[index] == CalculationConstants.Minus)
                    && bracketsLevel[index] == 0)
                {
                    lastOperationIndex = index;
                    break;
                }
                else if ((input[index] == CalculationConstants.Multiply || input[index] == CalculationConstants.Divide)
                        && bracketsLevel[index] == 0 && !multDivOperationHasBeenFound)
                {
                    lastOperationIndex = index;
                    multDivOperationHasBeenFound = true;
                }
                else if (input[index] == CalculationConstants.Power
                        && bracketsLevel[index] == 0 && !multDivOperationHasBeenFound && !powerOperationHasBeenFound)
                {
                    lastOperationIndex = index;
                    powerOperationHasBeenFound = true;
                }
            }

            return lastOperationIndex;
        }

        #endregion

    }
}