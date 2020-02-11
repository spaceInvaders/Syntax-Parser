using System;

namespace Syntax_Pars
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your phrase for calculation: ");
            string input = Console.ReadLine();
            Node<CalculationNode> myNode = input.GrowNodeTree();
            decimal result = myNode.Calculate();
            Console.WriteLine(result);
        }
    }
}