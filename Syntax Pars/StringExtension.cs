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
            int[] bracketsLevel = BracketsLevel(input: input);
            string right = null;
            string left = null;
            char operation = '\0';
            int multDivSetter = 0;
            int powerSetter = 0;
            for (int index = input.Length - 1; index >= 0; index--)
            {
                if (bracketsLevel[index] == 0 && (input[index] == Plus || input[index] == Minus))
                {
                    right = input.Substring(index + 1);
                    left = input.Substring(0, index);
                    operation = input[index];
                    break;
                }
                else if (bracketsLevel[index] == 0 && (input[index] == Multiply || input[index] == Divide) && multDivSetter == 0)
                {
                    right = input.Substring(index + 1);
                    left = input.Substring(0, index);
                    operation = input[index];
                    multDivSetter = 1;
                }
                else if (bracketsLevel[index] == 0 && input[index] == Power && multDivSetter == 0 && powerSetter == 0)
                {
                    right = input.Substring(index + 1);
                    left = input.Substring(0, index);
                    operation = input[index];
                    powerSetter = 1;
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