using System;

namespace Syntax_Pars
{
    enum Operation
    {
        Number,
        Add,
        Subtract,
        Multiply,
        Divide,
    }
    struct CalculationNode
    {
        public Operation Operation { get; set; }
        public decimal Number { get; set; }
    }
    class Node<T>
    {
        public Node<T> left;
        public Node<T> right;
        public CalculationNode info;
        internal decimal Calculate(Node<CalculationNode> node)
        {
            return node.info.Operation switch
            {
                Operation.Add => node.Calculate(node.left) + node.Calculate(node.right),
                Operation.Subtract => node.Calculate(node.left) - node.Calculate(node.right),
                Operation.Multiply => node.Calculate(node.left) * node.Calculate(node.right),
                Operation.Divide => node.Calculate(node.left) / node.Calculate(node.right),
                Operation.Number => Convert.ToDecimal(node.info.Number),
                _ => 0,
            };
        }
    }
    public static class StringExtension
    {
        internal static Node<CalculationNode> GrowNodeTree(this string input)
        {
            bool inputIsGood = CheckInput(input: input);
            Node<CalculationNode> node = null;
            if (inputIsGood)
            {
                TrimBrackets(input: input);
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == '+' || input[i] == '-' || input[i] == '/' || input[i] == '*')
                    {
                        node = SplitToNodes(input: input);
                        break;
                    }
                    else if (i == input.Length - 1)
                    {
                        node = new Node<CalculationNode>();
                        node.info.Operation = Operation.Number;
                        node.info.Number = Convert.ToDecimal(input);
                    }
                }
            }
            return node;
        }
        static bool CheckInput(string input)
        {
            input = input.Replace(" ", "");
            bool a = false;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if ((c != '0') && (c != '1') && (c != '2') && (c != '3') && (c != '4') && (c != '5')
                   && (c != '6') && (c != '7') && (c != '8') && (c != '9') && (c != '0') && (c != '+')
                   && (c != '-') && (c != '*') && (c != '/') && (c != '(') && (c != ')') && (c != ','))
                {
                    Console.WriteLine("Invalid input");
                    a = false;
                    break;
                }
                else if (i == input.Length - 1)
                {
                    Console.WriteLine("Input is ok");
                    a = true;
                }
            }
            return a;
        }
        static int[] BracketsLevel(string input)
        {
            int[] marker = new int[input.Length];
            if (input[0] == '(')
            {
                marker[0] = 1;
            }
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    marker[i] = marker[i - 1] + 1;
                }
                else if (input[i] == ')')
                {
                    marker[i] = marker[i - 1] - 1;
                }
                else
                {
                    marker[i] = marker[i - 1];
                }
            }
            return marker;
        }

        static string TrimBrackets(string input)
        {
            if (input[0] == '(' && input[input.Length - 1] == ')')
            {
                int[] marker = BracketsLevel(input: input);
                for (int i = 1; i < input.Length - 1; i++)
                {
                    if (marker[i] == 0)
                    {
                        return input;
                    }
                    else if (i == input.Length - 2)
                    {
                        input = input.Substring(1, input.Length - 2);
                    }
                }
            }
            if (input[0] == '(' && input[input.Length - 1] == ')')
            {
                TrimBrackets(input: input);
            }
            return input;
        }
        static Node<CalculationNode> SplitToNodes(this string input)
        {
            input = TrimBrackets(input: input);

            int[] marker = BracketsLevel(input: input);

            string right = null;
            string left = null;
            char operation = '\0';

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (marker[i] == 0 && (input[i] == '+' || input[i] == '-'))
                {
                    right = input.Substring(i + 1);
                    left = input.Substring(0, i);
                    operation = input[i];
                    break;
                }
                else if (i == 0)
                {
                    for (int j = input.Length - 1; j >= 0; j--)
                    {
                        if (marker[j] == 0 && (input[j] == '*' || input[j] == '/'))
                        {
                            right = input.Substring(j + 1);
                            left = input.Substring(0, j);
                            operation = input[j];
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
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] == '+' || left[i] == '-' || left[i] == '/' || left[i] == '*')
                {
                    node.left = left.SplitToNodes();
                    break;
                }
                else if (i == left.Length - 1)
                {
                    node.left = new Node<CalculationNode>();
                    left = TrimBrackets(input: left);
                    node.left.info.Number = Convert.ToDecimal(left);
                }
            }
            for (int i = 0; i < right.Length; i++)
            {
                if (right[i] == '+' || right[i] == '-' || right[i] == '/' || right[i] == '*')
                {
                    node.right = right.SplitToNodes();
                    break;
                }
                else if (i == right.Length - 1)
                {
                    node.right = new Node<CalculationNode>();
                    right = TrimBrackets(input: right);
                    node.right.info.Number = Convert.ToDecimal(right);
                }
            }
            return node;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your phrase for calculation: ");
            string a = Console.ReadLine();
            Node<CalculationNode> myNode = a.GrowNodeTree();
            decimal result = myNode.Calculate(myNode);
            Console.WriteLine(result);
        }
    }
}