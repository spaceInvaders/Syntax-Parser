using System;

namespace Syntax_Pars
{
    public static partial class StringExtensionBase
    {
        internal static Node<CalculationNode> GrowNodeTree(this string input)
        {
            string editedInput = CheckInput(input: input);
            Node<CalculationNode> node = null;
            if (editedInput != null)
            {
                TrimBrackets(input: editedInput);
                if (editedInput.Contains('+') || editedInput.Contains('-') || editedInput.Contains('/') || editedInput.Contains('*'))
                {
                    node = editedInput.SplitToNodes();
                }
                else
                {
                    editedInput = TrimBrackets(input: editedInput);
                    node = new Node<CalculationNode>();
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
            editedInput = editedInput.CheckOnMinus();
            editedInput = editedInput.CheckOnBrackets();
            editedInput = editedInput?.CheckOnComma();
            editedInput = editedInput?.CheckOnFigures();
            if (editedInput != null)
            {
                Console.WriteLine("Input is ok");
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
            return editedInput;
        }

        internal static Node<CalculationNode> SplitToNodes(this string input)
        {
            input = TrimBrackets(input: input);
            int[] marker = BracketsLevel(input: input);
            string right = null;
            string left = null;
            char operation = '\0';
            for (int index = input.Length - 1; index >= 0; index--)
            {
                if (marker[index] == 0 && (input[index] == '+' || input[index] == '-'))
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
                        if (marker[currentIndex] == 0 && (input[currentIndex] == '*' || input[currentIndex] == '/'))
                        {
                            right = input.Substring(currentIndex + 1);
                            left = input.Substring(0, currentIndex);
                            operation = input[currentIndex];
                        }
                    }
                }
            }
            Node<CalculationNode> node = new Node<CalculationNode>();
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
            if (left.Contains('+') || left.Contains('-') || left.Contains('/') || left.Contains('*'))
            {
                node.Left = left.SplitToNodes();
            }
            else
            {
                node.Left = new Node<CalculationNode>();
                left = TrimBrackets(input: left);
                node.Left.info.Number = Convert.ToDecimal(left);
            }
            if (right.Contains('+') || right.Contains('-') || right.Contains('/') || right.Contains('*'))
            {
                node.Right = right.SplitToNodes();
            }
            else
            {
                node.Right = new Node<CalculationNode>();
                right = TrimBrackets(input: right);
                node.Right.info.Number = Convert.ToDecimal(right);
            }
            return node;
        }
    }
}
