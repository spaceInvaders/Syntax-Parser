using System;

namespace Syntax_Pars
{
    class Node
    {
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
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if ((c != '0') && (c != '1') && (c != '2') && (c != '3') && (c != '4') && (c != '5')
                   && (c != '6') && (c != '7') && (c != '8') && (c != '9') && (c != '0') && (c != '+')
                   && (c != '-') && (c != '*') && (c != '/') && (c != '(') && (c != ')') && (c != '.'))
                {
                    Console.WriteLine("Invalid input");
                    break;
                }
                else if (i == input.Length - 1)
                {
                    Phrase = input;
                    Console.WriteLine("Input is ok");
                }
            }
        }
        public int[] BracketsLevel()
        {
            int[] marker = new int[phrase.Length];
            if (phrase[0] == '(')
            {
                marker[0] = 1;
            }
            for (int i = 1; i < phrase.Length; i++)
            {
                if (phrase[i] == '(')
                {
                    marker[i] = marker[i - 1] + 1;
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
            return marker;
        }

        public void TrimBrackets()
        {
            if (phrase[0] == '(' && phrase[phrase.Length - 1] == ')')
            {
                int[] marker = BracketsLevel();
                for (int i = 1; i < phrase.Length - 1; i++)
                {
                    if (marker[i] == 0)
                    {
                        return;
                    }
                    else if (i == phrase.Length - 2)
                    {
                        phrase = phrase.Substring(1, phrase.Length - 2);
                    }
                }
            }
            if (phrase[0] == '(' && phrase[phrase.Length - 1] == ')')
            {
                TrimBrackets();
            }
        }
        public void Execute()
        {
            CheckInput();
            TrimBrackets();
            for (int i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == '+' || phrase[i] == '-' || phrase[i] == '/' || phrase[i] == '*')
                {
                    SplitToNodes();
                    Console.WriteLine(Calculate());
                }
                else if (i == phrase.Length - 1)
                {
                    Console.WriteLine(Convert.ToDouble(phrase));
                }
            }
        }
        public void SplitToNodes()
        {
            TrimBrackets();

            int[] marker = BracketsLevel();
            
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
                if (this.right.phrase[i] == '+' || this.right.phrase[i] == '-' || this.right.phrase[i] == '/' || this.right.phrase[i] == '*')
                {
                    this.right.SplitToNodes();
                }
            }
        }
        public decimal Calculate()
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
                    return Convert.ToDecimal(left.phrase) + Convert.ToDecimal(right.phrase);

                case "-":
                    return Convert.ToDecimal(left.phrase) - Convert.ToDecimal(right.phrase);

                case "*":
                    return Convert.ToDecimal(left.phrase) * Convert.ToDecimal(right.phrase);

                case "/":
                    return Convert.ToDecimal(left.phrase) / Convert.ToDecimal(right.phrase);

                default:
                    return Convert.ToDecimal(phrase);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your phrase for calculation: ");
            Node tree = new Node();
            tree.Execute();
        }
    }
}
