using System;
using System.Globalization;
using CalculatorCore;

namespace ConsoleCalculator
{
    internal class Program
    {
        private delegate void DecimalResultRoundingHandler();
        private static event DecimalResultRoundingHandler Notify;

        private const int PrecisionForDecimalResult = 15;
        private const int PrecisionForBinaryResult = 15;
        private static char Separator(CultureInfo culture) => Convert.ToChar(culture.NumberFormat.NumberDecimalSeparator);

        private static void Main(string[] args)

        {
            DisplayStartInfo();

            while (true)
            {
                string input = null;

                while (String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Calculate this: ");
                    Console.ForegroundColor = ConsoleColor.White;

                    input = Console.ReadLine();
                }

                if (input == "q")
                    break;

                ExecuteExpression(input: input, culture: CultureInfo.CurrentCulture);
            }
        }

        internal static string ExecuteExpression(string input, CultureInfo culture)
        {
            string decimalResult = null;

            try
            {
                var myNode = CalculationParser.GrowNodeTree(input: input, culture: culture);

                if (myNode != null)
                {
                    decimal result = DisplayDecimalResult(node: myNode, culture: culture);
                    decimalResult = result.ToString($"n{PrecisionForDecimalResult}", culture);
                    decimalResult = decimalResult.TrimEnd('0').TrimEnd(Separator(culture: culture));

                    string binaryResult = Convertions.ConvertDecimalToBinaryString
                        (input: result, roundingPrecisionForBinary: PrecisionForBinaryResult, culture: culture);

                    DisplayBinaryResult(input: result, culture: culture);

                    Console.ForegroundColor = ConsoleColor.White;

                    Notify?.Invoke();
                    Notify = null;
                }
            }
            catch (CheckingException exception)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Error: " + exception.Message);
            }
            catch (DivideByZeroException)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Error: Divide by Zero");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Error: Too large or too small value");
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Error: Calculation failed");
            }

            return decimalResult;
        }

        #region PrivateMethods

        private static void DisplayStartInfo()
        {
            char separator = Separator(culture: CultureInfo.CurrentCulture);

            ConsoleColor color = Console.ForegroundColor;
            Console.WriteLine($"Hi, u can calculate smth like this:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"-4{separator}01*p^(((32^1/2)))-(22{separator}5/5-1{separator}2^3)");
            Console.ForegroundColor = color;
            Console.WriteLine($"Note, your decimal separator is '{separator}'");
            Console.WriteLine("Result - press 'enter', leave - press 'q'");
        }

        private static decimal DisplayDecimalResult(ICalculationOperation node, CultureInfo culture)
        {
            decimal result = node.Calculate();
            string decimalResult = result.ToString($"n{PrecisionForDecimalResult}", culture);

            decimalResult = decimalResult.TrimEnd('0').TrimEnd(Separator(culture: culture));

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("decimal: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(decimalResult);

            int separatorIndex = result.ToString().IndexOf(Separator(culture: culture));

            if (separatorIndex > 0 && result.ToString().Length - separatorIndex > PrecisionForDecimalResult)
                Notify += () => Console.WriteLine($"Note, decimal result is rounded, precision is '{PrecisionForDecimalResult}'");

            return result;
        }

        private static void DisplayBinaryResult(decimal input, CultureInfo culture)
        {
            string binaryResult = Convertions.ConvertDecimalToBinaryString
                        (input: input, roundingPrecisionForBinary: PrecisionForBinaryResult, culture: culture);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("binary: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(binaryResult);
        }

        #endregion

    }
}