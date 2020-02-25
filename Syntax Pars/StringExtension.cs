using System;
using System.Collections.Generic;
using System.Linq;

namespace Syntax_Pars
{
    public static partial class StringExtension
    {
        internal static Node<CalculationElement> GrowNodeTree(this string input)
        {
            string editedInput = CheckInput(input: input);
            Node<CalculationElement> node = null;
            if (editedInput != null)
            {
                if(editedInput.Any(character => "+-*/".Contains(character)))
                { 
                    node = editedInput.SplitToNodes();
                }
                else
                {
                    node = new Node<CalculationElement>();
                    node.info.Operation = Operation.Number;
                    node.info.Number = Convert.ToDecimal(editedInput);
                }
            }
            return node;
        }
    

        public static string CheckInput(string input)
        {
            string editedInput = input.Replace(".", ",");
            editedInput = editedInput.Replace(" ", "");
            if (editedInput == "")
            {
                editedInput = null;
            }
            editedInput?.CheckOnBrackets();
            editedInput?.CheckOnComma();
            editedInput?.CheckOnFigures();
            editedInput?.CheckOnOperations();
            editedInput = editedInput?.ValidatedUnaryMinusString();
            return editedInput;
        }

        internal static Node<CalculationElement> SplitToNodes(this string input)
        {
            input = TrimBrackets(input: input);
            int[] marker = BracketsLevel(input: input);
            string right = null;
            string left = null;
            char operation = '\0';
            for (int index = input.Length - 1; index >= 0; index--)
            {
                if (marker[index] == 0 && input[index] == '+' ||
                    marker[index] == 0 && input[index] == '-')
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
                        if (marker[currentIndex] == 0 && input[currentIndex] == '*' ||
                            marker[currentIndex] == 0 && input[currentIndex] == '/')
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
                case '+':
                    node.info.Operation = Operation.Add;
                    break;
                case '-':
                    node.info.Operation = Operation.Subtract;
                    break;
                case '/':
                    node.info.Operation = Operation.Divide;
                    break;
                case '*':
                    node.info.Operation = Operation.Multiply;
                    break;
            }
            if (left.Any(character => "+-*/".Contains(character)))
            {
                node.Left = left.SplitToNodes();
            }
            else
            {
                node.Left = new Node<CalculationElement>();
                left = TrimBrackets(input: left);
                node.Left.info.Number = Convert.ToDecimal(left);
            }
            if (right.Any(character => "+-*/".Contains(character)))
            {
                node.Right = right.SplitToNodes();
            }
            else
            {
                node.Right = new Node<CalculationElement>();
                right = TrimBrackets(input: right);
                node.Right.info.Number = Convert.ToDecimal(right);
            }
            return node;
        }
    }
}
