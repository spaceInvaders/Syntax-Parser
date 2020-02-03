using System;

namespace Syntax_Pars
{
    class Node
    {
        double result;
        Node left;
        Node right;
        private string phrase;
        public string Phrase
        {
            get
            {
                return phrase;
            }
            set
            {
                phrase = value;
            }
        }
        public void CheckInput()
        {
            string input = Console.ReadLine();
            input = input.Replace(" ", "");
            char[] elements = input.ToCharArray();
            for (int i = 0; i < elements.Length; i++)
            {
                char c = elements[i];
                if ((c != '0') && (c != '1') && (c != '2') && (c != '3') && (c != '4') && (c != '5')
                   && (c != '6') && (c != '7') && (c != '8') && (c != '9') && (c != '0') && (c != '+')
                   && (c != '-') && (c != '*') && (c != '/') && (c != '(') && (c != ')') && (c != '.'))
                {
                    Console.WriteLine("Invalid input");
                    break;
                }
                else if (i == elements.Length - 1)
                {
                    Phrase = input;
                    Console.WriteLine("Input is ok");
                }
            }
        }
        public void TrimBrackets()
        {
            if (phrase[0] == '(' && phrase[phrase.Length - 1] == ')')
            {
                for (int i = 1; i < phrase.Length - 1; i++)
                {
                    if (phrase[i] == ')' || phrase[i] == '(')
                    {
                        break;
                    }
                    else if (i == phrase.Length - 2)
                    {
                        phrase = phrase.Substring(1, phrase.Length - 2);
                    }
                }
            }
        }
        public void CheckOnOPerations()
        {
            TrimBrackets();
            for (int i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == '+' || phrase[i] == '-' || phrase[i] == '/' || phrase[i] == '*')
                {
                    SplitToNodes();
                }
                else if (i == phrase.Length - 1)
                {
                    result = Convert.ToDouble(phrase);
                }
            }
        }
        public void DisplayResult()
        {
            Console.WriteLine(result);
        }
        public void Execute()
        {
            CheckOnOPerations();
            DisplayResult();
        }
        public void SplitToNodes()
        {
            TrimBrackets();

            int[] marker = new int[phrase.Length];
            if (phrase[0] != '(')
            {
                marker[0] = 0;
            }
            else if (phrase[0] == '(')
            {
                marker[0] = 1;
            }
            for (int i = 1; i < phrase.Length; i++)
            {
                if (phrase[i] == '(')
                {
                    marker[i] += marker[i - 1] + 1;
                }
                else if (phrase[i] == ')')
                {
                    marker[i] = marker[i - 1] - 1;
                }
                else
                {
                    marker[i] = marker[i - 1];
                }
            }
            string right = null;
            string left = null;
            string sign = null;

            for (int i = 0; i < phrase.Length; i++)
            {
                if (marker[i] == 0)
                {
                    if (phrase[i] == '+' || phrase[i] == '-')
                    {
                        right = phrase.Substring(i + 1);
                        left = phrase.Substring(0, i);
                        sign = new String(new char[] { phrase[i] });
                        break;
                    }
                    else if (phrase[i] == '*' || phrase[i] == '/')
                    {
                        right = phrase.Substring(i + 1);
                        left = phrase.Substring(0, i);
                        sign = new String(new char[] { phrase[i] });
                        break;
                    }
                    else if (phrase[i] == '*' || phrase[i] == '/')
                    {
                        right = phrase.Substring(i + 1);
                        left = phrase.Substring(0, i);
                        sign = new String(new char[] { phrase[i] });
                        break;
                    }
                }
            }
            this.left = new Node() { Phrase = left };
            this.right = new Node() { Phrase = right };
            this.Phrase = sign;
            for (int i = 0; i < this.left.phrase.Length; i++)
            {
                if (this.left.phrase[i] == '+' || this.left.phrase[i] == '-' || this.left.phrase[i] == '/' || this.left.phrase[i] == '*')
                {
                    this.left.SplitToNodes();
                }
            }
            for (int i = 0; i < this.right.phrase.Length; i++)
            {
                if (this.right.phrase[i] == '+' || this.left.phrase[i] == '-' || this.left.phrase[i] == '/' || this.left.phrase[i] == '*')
                {
                    this.right.SplitToNodes();
                }
            }
            Calculate();
        }
        public void Calculate()
        {
            if (left?.phrase == "-" || left?.phrase == "+" || left?.phrase == "*" || left?.phrase == "/")
            {
                left.Calculate();
            }
            if (right?.phrase == "-" || right?.phrase == "+" || right?.phrase == "*" || right?.phrase == "/")
            {
                right.Calculate();
            }
            switch (phrase)
            {
                case "+":
                    result = Convert.ToDouble(left.phrase) + Convert.ToDouble(right.phrase);
                    phrase = Convert.ToString(result);
                    break;
                case "-":
                    result = Convert.ToDouble(left.phrase) - Convert.ToDouble(right.phrase);
                    phrase = Convert.ToString(result);
                    break;
                case "*":
                    result = Convert.ToDouble(left.phrase) * Convert.ToDouble(right.phrase);
                    phrase = Convert.ToString(result);
                    break;
                case "/":
                    result = Convert.ToDouble(left.phrase) / Convert.ToDouble(right.phrase);
                    phrase = Convert.ToString(result);
                    break;
                default:
                    result = Convert.ToDouble(phrase);
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your phrase for calculation: ");
            Node tree = new Node();
            tree.CheckInput();
            tree.Execute();
        }
    }
}
