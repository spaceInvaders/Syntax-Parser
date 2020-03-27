using System;
using System.Linq;
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
                Console.WriteLine("Enter your phrase for calculation:");
                Console.WriteLine($"(Note, your decimal separator is '{StringExtension.Separator(culture: CultureInfo.CurrentCulture)}')");
                input = Console.ReadLine();
            }
            Solve(input: input, culture: CultureInfo.CurrentCulture);
        }

        internal static string Solve(string input, CultureInfo culture)
        {
            const int PrecisionForDecimalResult = 15;
            const int PrecisionForBinaryResult = 15;
            string decimalResult = null;
            try
            {
                Node<CalculationElement> myNode = input.GrowNodeTree(culture: culture);
                if (myNode != null)
                {
                    decimal result = myNode.Calculate();
                    decimalResult = result.ToString($"n{PrecisionForDecimalResult}", culture);
                    decimalResult = decimalResult.TrimEnd('0').TrimEnd(StringExtension.Separator(culture: culture));
                    Console.WriteLine(decimalResult);
                    int separatorIndex = result.ToString().IndexOf(StringExtension.Separator(culture: culture));
                    if (separatorIndex > 0 && result.ToString().Length - separatorIndex > PrecisionForDecimalResult)
                        Console.WriteLine($"(Note, decimal result is rounded, precision is '{PrecisionForDecimalResult}')");
                    string binaryResult = "0b: " + Convertions.ConvertDecimalToBinaryString
                        (input: result, roundingPrecisionForBinary: PrecisionForBinaryResult, culture: culture);
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