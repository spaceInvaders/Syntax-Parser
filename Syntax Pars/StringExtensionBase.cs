using System;
using System.Globalization;
using System.Linq;

namespace Syntax_Pars
{
    static partial class StringExtension
    {
        internal static Node<CalculationElement> GrowNodeTree(this string input)
        {
            string editedInput = CheckInput(input: input);
            Node<CalculationElement> node = null;
            if (editedInput.Any(character => PlusMinMultDivPow.Contains(character)))
            {
                node = editedInput.SplitToNodes();
            }
            else
            {
                node = new Node<CalculationElement>();
                node.info.Operation = Operation.Number;
                node.info.Number = decimal.Parse(editedInput, new CultureInfo("uk-UA"));
            }
            return node;
        }

        internal static string CheckInput(string input)
        {
            string editedInput = input.Replace(".", ",");
            editedInput = editedInput.Replace(" ", String.Empty);
            editedInput.CheckOnBrackets();
            editedInput.CheckOnSeparator();
            editedInput.CheckOnValidatedFigures();
            editedInput.CheckOnOperations();
            editedInput = editedInput.TrimBracketsString();
            editedInput = editedInput.ValidatedUnaryMinusString();
            editedInput = editedInput.TrimExcessiveZerosString();
            return editedInput;
        }

        internal static Node<CalculationElement> SplitToNodes(this string input)
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
                node.Left = left.SplitToNodes();
            }
            else
            {
                node.Left = new Node<CalculationElement>();
                left = left.TrimBracketsString();
                node.Left.info.Number = decimal.Parse(left, new CultureInfo("uk-UA"));
            }
            if (right.Any(character => PlusMinMultDivPow.Contains(character)))
            {
                node.Right = right.SplitToNodes();
            }
            else
            {
                node.Right = new Node<CalculationElement>();
                right = right.TrimBracketsString();
                node.Right.info.Number = decimal.Parse(right, new CultureInfo("uk-UA"));
            }
            return node;
        }
    }
}