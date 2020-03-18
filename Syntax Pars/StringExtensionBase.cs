using System;
using System.Globalization;
using System.Linq;

namespace Syntax_Pars
{
    static partial class StringExtension
    {
        internal static Node<CalculationElement> GrowNodeTree(this string input, CultureInfo culture)
        {
            string editedInput = CheckInput(input: input, culture: culture);
            Node<CalculationElement> node = null;
            if (editedInput.Any(character => PlusMinMultDivPow.Contains(character)))
            {
                node = editedInput.SplitToNodes(culture: culture);
            }
            else
            {
                node = new Node<CalculationElement>();
                node.info.Operation = Operation.Number;
                node.info.Number = decimal.Parse(editedInput, culture);
            }
            return node;
        }

        internal static string CheckInput(string input, CultureInfo culture)
        {
            char Separator = Convert.ToChar(culture.NumberFormat.NumberDecimalSeparator);
            string editedInput = input.Replace(" ", String.Empty);
            if (Separator != Comma)
            {
                editedInput = input.Replace(Comma.ToString(), String.Empty);
            }
            editedInput = editedInput.ParseInputString(culture: culture);
            editedInput = editedInput.TrimBracketsString();
            editedInput = editedInput.Replace(PiChar.ToString(), Math.PI.ToString(culture));
            return editedInput;
        }

        internal static Node<CalculationElement> SplitToNodes(this string input, CultureInfo culture)
        {
            input = TrimBracketsString(input: input);
            string right = null;
            string left = null;
            char operation = '\0';
            /* Since people read from left to right, to ensure the correct order of operations,
             * node tree should be built from right to left with priority of operations outside brackets:
             * plus or minus, multply or divide, power */
            int lastOperationIndex = input.FindLastOerationWithPriorityPlusMinus();
            right = input.Substring(lastOperationIndex + 1);
            left = input.Substring(0, lastOperationIndex);
            operation = input[lastOperationIndex];
            Node<CalculationElement> node = new Node<CalculationElement>();
            switch (operation)
            {
                case Plus:
                    node.info.Operation = Operation.Addition;
                    break;
                case Minus:
                    node.info.Operation = Operation.Subtraction;
                    break;
                case Divide:
                    node.info.Operation = Operation.Division;
                    break;
                case Multiply:
                    node.info.Operation = Operation.Multiplication;
                    break;
                case Power:
                    node.info.Operation = Operation.ToThePower;
                    break;
            }
            if (left.Any(character => PlusMinMultDivPow.Contains(character)))
            {
                node.Left = left.SplitToNodes(culture: culture);
            }
            else
            {
                node.Left = new Node<CalculationElement>();
                left = left.TrimBracketsString();
                node.Left.info.Number = decimal.Parse(left, culture);
            }
            if (right.Any(character => PlusMinMultDivPow.Contains(character)))
            {
                node.Right = right.SplitToNodes(culture: culture);
            }
            else
            {
                node.Right = new Node<CalculationElement>();
                right = right.TrimBracketsString();
                node.Right.info.Number = decimal.Parse(right, culture);
            }
            return node;
        }
    }
}