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
            Solve(input: input, culture: CultureInfo.CurrentCulture);
        }
  
        internal static string Solve(string input, CultureInfo culture)
        {
            string decimalResult = null;
            try
            {
                Node<CalculationElement> myNode = input.GrowNodeTree(culture: culture);
                if (myNode != null)
                {
                    decimal result = myNode.Calculate();
                    decimalResult = result.ToString("n15", culture);
                    string separator = culture.NumberFormat.NumberDecimalSeparator;
                    decimalResult = decimalResult.TrimEnd('0').TrimEnd(Convert.ToChar(separator));
                    Console.WriteLine(decimalResult);
                    string binaryResult = "0b: " + Convertions.ConvertDecimalToBinaryString(input: result, roundingPrecision: 5, culture: culture);
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
                Console.WriteLine("Error: Too large or too small value");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Calculation failed");
            }
            return decimalResult;
        }
    }
}