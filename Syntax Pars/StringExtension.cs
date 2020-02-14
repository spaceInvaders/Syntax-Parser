using System;

namespace Syntax_Pars
{
    public static class StringExtension
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
            for (int index = 0; index < editedInput.Length; index++)
            {
                char myChar = editedInput[index];
                if ((myChar != '0') && (myChar != '1') && (myChar != '2') && (myChar != '3') && (myChar != '4') && (myChar != '5')
                   && (myChar != '6') && (myChar != '7') && (myChar != '8') && (myChar != '9') && (myChar != '0') && (myChar != '+')
                   && (myChar != '-') && (myChar != '*') && (myChar != '/') && (myChar != '(') && (myChar != ')') && (myChar != ','))
                {
                    Console.WriteLine("Invalid input");
                    editedInput = null;
                    break;
                }
                else if (index == input.Length - 1)
                {
                    Console.WriteLine("Input is ok");
                }
            }
            return editedInput;
        }

        public static string CheckOnMinus(this string input)
        {
            if (input[0] == '-' || input[0] == '+')
            {
                input = "0" + input;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if ((input[index] == '-' || input[index] == '+') && input[index - 1] == '(')
                    input = input.Insert(index, "0");
            }
            return input;
        }

        public static int[] BracketsLevel(string input)
        {
            int[] marker = new int[input.Length];
            if (input[0] == '(')
            {
                marker[0] = 1;
            }
            for (int index = 1; index < input.Length; index++)
            {
                if (input[index] == '(')
                {
                    marker[index] = marker[index - 1] + 1;
                }
                else if (input[index] == ')')
                {
                    marker[index] = marker[index - 1] - 1;
                }
                else
                {
                    marker[index] = marker[index - 1];
                }
            }
            return marker;
        }

        public static string TrimBrackets(string input)
        {
            if (input[0] == '(' && input[input.Length - 1] == ')')
            {
                int[] marker = BracketsLevel(input: input);
                for (int index = 1; index < input.Length - 1; index++)
                {
                    if (marker[index] == 0)
                    {
                        return input;
                    }
                    else if (index == input.Length - 2)
                    {
                        input = input.Substring(1, input.Length - 2);
                    }
                }
            }
            if (input[0] == '(' && input[input.Length - 1] == ')')
            {
                input = TrimBrackets(input: input);
            }
            return input;
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
