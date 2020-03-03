using System;
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
                node.info.Number = Convert.ToDecimal(editedInput);
            }
            return node;
        }

        internal static string CheckInput(string input)
        {
            string editedInput = input.Replace(".", ",");
            editedInput = editedInput.Replace(" ", String.Empty);
            editedInput.CheckOnBrackets();
            editedInput.CheckOnComma();
            editedInput.CheckOnFigures();
            editedInput.CheckOnOperations();
            editedInput = editedInput.TrimBracketsString();
            editedInput = editedInput.ValidatedUnaryMinusString();
            editedInput = editedInput.TrimExcessiveZerosString();
            return editedInput;
        }

        internal static Node<CalculationElement> SplitToNodes(this string input)
        {
            input = TrimBracketsString(input: input);
            int[] marker = BracketsLevel(input: input);
            string right = null;
            string left = null;
            char operation = '\0';

            for (int plusMinusIndex = input.Length - 1; plusMinusIndex >= 0; plusMinusIndex--)
            {
                if (marker[plusMinusIndex] == 0 && input[plusMinusIndex] == Plus ||
                    marker[plusMinusIndex] == 0 && input[plusMinusIndex] == Minus)
                {
                    right = input.Substring(plusMinusIndex + 1);
                    left = input.Substring(0, plusMinusIndex);
                    operation = input[plusMinusIndex];
                    break;
                }
                else if (plusMinusIndex == 0)
                {
                    for (int multDivIndex = input.Length - 1; multDivIndex >= 0; multDivIndex--)
                    {
                        if (marker[multDivIndex] == 0 && input[multDivIndex] == Multiply ||
                            marker[multDivIndex] == 0 && input[multDivIndex] == Divide)
                        {
                            right = input.Substring(multDivIndex + 1);
                            left = input.Substring(0, multDivIndex);
                            operation = input[multDivIndex];
                            break;
                        }
                        else if (plusMinusIndex == 0 && multDivIndex == 0)
                        {
                            for (int powerIndex = input.Length - 1; powerIndex >= 0; powerIndex--)
                            {
                                if (marker[powerIndex] == 0 && input[powerIndex] == Power)
                                {
                                    right = input.Substring(powerIndex + 1);
                                    left = input.Substring(0, powerIndex);
                                    operation = input[powerIndex];
                                    break;
                                }
                            }
                        }
                    }
                }
            }
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
                node.Left.info.Number = Convert.ToDecimal(left);
            }
            if (right.Any(character => PlusMinMultDivPow.Contains(character)))
            {
                node.Right = right.SplitToNodes();
            }
            else
            {
                node.Right = new Node<CalculationElement>();
                right = right.TrimBracketsString();
                node.Right.info.Number = Convert.ToDecimal(right);
            }
            return node;
        }
    }
}
