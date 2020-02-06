using System;

namespace Syntax_Pars
{
    enum Essence
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Number
    }
    class Node
    {
        Node left;
        Node right;
        Essence eseence;
        public string Phrase { get; set; }
        public void Pars()
        {
            string input = Console.ReadLine();
            input = input.Replace(" ", "");
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if ((c != '0') && (c != '1') && (c != '2') && (c != '3') && (c != '4') && (c != '5')
                   && (c != '6') && (c != '7') && (c != '8') && (c != '9') && (c != '0') && (c != '+')
                   && (c != '-') && (c != '*') && (c != '/') && (c != '(') && (c != ')') && (c != ','))
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
            int[] marker = new int[Phrase.Length];
            if (Phrase[0] == '(')
            {
                marker[0] = 1;
            }
            for (int i = 1; i < Phrase.Length; i++)
            {
                if (Phrase[i] == '(')
                {
                    marker[i] = marker[i - 1] + 1;
                }
                else if (Phrase[i] == ')')
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
            if (Phrase[0] == '(' && Phrase[Phrase.Length - 1] == ')')
            {
                int[] marker = BracketsLevel();
                for (int i = 1; i < Phrase.Length - 1; i++)
                {
                    if (marker[i] == 0)
                    {
                        return;
                    }
                    else if (i == Phrase.Length - 2)
                    {
                        Phrase = Phrase.Substring(1, Phrase.Length - 2);
                    }
                }
            }
            if (Phrase[0] == '(' && Phrase[Phrase.Length - 1] == ')')
            {
                TrimBrackets();
            }
        }
        public void Execute()
        {
            Pars();
            TrimBrackets();
            for (int i = 0; i < Phrase.Length; i++)
            {
                if (Phrase[i] == '+' || Phrase[i] == '-' || Phrase[i] == '/' || Phrase[i] == '*')
                {
                    SplitToNodes();
                    Console.WriteLine(Count());
                }
                else if (i == Phrase.Length - 1)
                {
                    Console.WriteLine(Convert.ToDouble(Phrase));
                }
            }
        }
        public void SplitToNodes()
        {
            TrimBrackets();
            int[] marker = BracketsLevel();
            for (int i = Phrase.Length - 1; i > -1; i--)
            {
                if (marker[i] == 0 && (Phrase[i] == '+' || Phrase[i] == '-' || Phrase[i] == '*' || Phrase[i] == '/'))
                {
                    left = new Node() { Phrase = Phrase.Substring(0, i) };
                    right = new Node() { Phrase = Phrase.Substring(i + 1) };
                    if (Phrase[i] == '+')
                    {
                        eseence = Essence.Add;
                        break;
                    }
                    else if (Phrase[i] == '-')
                    {
                        eseence = Essence.Subtract;
                        break;
                    }
                    else if (Phrase[i] == '*')
                    {
                        eseence = Essence.Multiply;
                        break;
                    }
                    else if (Phrase[i] == '/')
                    {
                        eseence = Essence.Divide;
                        break;
                    }
                }
                else if (i == 0)
                {
                    eseence = Essence.Number;
                }
            }
            for (int i = 0; i < left.Phrase.Length; i++)
            {
                if (left.Phrase[i] == '+' || left.Phrase[i] == '-' || left.Phrase[i] == '/' || left.Phrase[i] == '*')
                {
                    left.SplitToNodes();
                }
            }
            for (int i = 0; i < right.Phrase.Length; i++)
            {
                if (right.Phrase[i] == '+' || right.Phrase[i] == '-' || right.Phrase[i] == '/' || right.Phrase[i] == '*')
                {
                    right.SplitToNodes();
                }
            }
        }
        public decimal Count()
        {
            switch (eseence)
            {
                case Essence.Add:
                    return left.Count() + right.Count();
                case Essence.Subtract:
                    return left.Count() - right.Count();
                case Essence.Multiply:
                    return left.Count() * right.Count();
                case Essence.Divide:
                    return left.Count() / right.Count();
                case Essence.Number:
                    TrimBrackets();
                    return Convert.ToDecimal(eseence);
                default: return 0;
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
