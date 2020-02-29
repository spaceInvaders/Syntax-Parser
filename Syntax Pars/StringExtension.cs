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
            if(editedInput.Any(character => PlusMinMultDiv.Contains(character)))
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
            editedInput = editedInput.Replace(" ", "");
            editedInput.CheckOnBrackets();
            editedInput.CheckOnComma();
            editedInput.CheckOnFigures();
            editedInput.CheckOnOperations();
            editedInput = TrimBracketsString(input: editedInput);
            editedInput = editedInput.ValidatedUnaryMinusString();
            return editedInput;
        }

        internal static Node<CalculationElement> SplitToNodes(this string input)
        {
            input = TrimBracketsString(input: input);
            int[] marker = BracketsLevel(input: input);
            string right = null;
            string left = null;
            char operation = '\0';
            for (int index = input.Length - 1; index >= 0; index--)
            {
                if (marker[index] == 0 && input[index] == Plus ||
                    marker[index] == 0 && input[index] == Minus)
                {
                    right = input.Substring(index + 1);
                    left = input.Substring(0, index);
                    operation = input[index];
                    break;
                }
                else if (index == 0)
                {
                    for (int currentIndex = input.Length - 1; currentIndex >= 0; currentIndex--)
                    {
                        if (marker[currentIndex] == 0 && input[currentIndex] == Multiply ||
                            marker[currentIndex] == 0 && input[currentIndex] == Divide)
                        {
                            right = input.Substring(currentIndex + 1);
                            left = input.Substring(0, currentIndex);
                            operation = input[currentIndex];
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
            }
            if (left.Any(character => PlusMinMultDiv.Contains(character)))
            {
                node.Left = left.SplitToNodes();
            }
            else
            {
                node.Left = new Node<CalculationElement>();
                left = TrimBracketsString(input: left);
                node.Left.info.Number = Convert.ToDecimal(left);
            }
            if (right.Any(character => PlusMinMultDiv.Contains(character)))
            {
                node.Right = right.SplitToNodes();
            }
            else
            {
                node.Right = new Node<CalculationElement>();
                right = TrimBracketsString(input: right);
                node.Right.info.Number = Convert.ToDecimal(right);
            }
            return node;
        }
    }
}
