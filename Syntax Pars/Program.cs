using System;

namespace Syntax_Pars
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;
            while (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Enter your phrase for calculation: ");
                input = Console.ReadLine();
                Solve(input);
            }
        }
        internal static string Solve(string input)
        {
            string output = null;
            try
            {
                Node<CalculationElement> myNode = input.GrowNodeTree();
                if (myNode != null)
                {
                    decimal result = myNode.Calculate();
                    decimal binaryResult = Convertions.ConvertToBinary(input: result);
                    Console.WriteLine(result);
                    Console.WriteLine("0b " + binaryResult);
                    output = result.ToString();
                }
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Divide by Zero");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Calculation failed");
            }
            return output;
        }
    }
}