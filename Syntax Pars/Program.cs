using System;
using System.Globalization;

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
            }
            Solve(input);
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
                    Console.WriteLine(result);
                    output = result.ToString(new CultureInfo("uk-UA"));
                    string binaryResult = "0b: " + Convertions.ConvertDecimalToBinaryString(input: result, roundingPrecision: 5);
                    Console.WriteLine(binaryResult);
                }
            }
            catch (ParsingException exception)
            {
                Console.WriteLine("Error: " + exception.Message);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Divide by Zero");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: Value was too large or too small");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Calculation failed");
            }
            return output;
        }
    }
}