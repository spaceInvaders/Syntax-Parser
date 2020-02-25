using System;
using System.Collections.Generic;

namespace Syntax_Pars
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;
            while (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Enter your phrase for calculation: ");
                input = Console.ReadLine();
            }
            try
            {
                Node<CalculationElement> myNode = input.GrowNodeTree();
                if (myNode != null)
                {

                    decimal result = myNode.Calculate();
                    Console.WriteLine(result);
                }
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Divide by Zero");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Calculation failed");
            }
        }
    }
}