using CalculatorCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace WebApplicationCalculator.Models
{
    public class CalcInput
    {
        private const int PrecisionForDecimalResult = 15;
        private const int PrecisionForBinaryResult = 15;

        [JsonConstructor]
        internal CalcInput(string expression, CultureInfo culture)
        {
            Expression = expression;
            Culture = culture;
        }

        internal string Expression { get; private set; }
        internal CultureInfo Culture { get; private set; }

        private char Separator(CultureInfo culture) => Convert.ToChar(culture.NumberFormat.NumberDecimalSeparator);

        internal CalcResult ExecuteExpression()
        {
            string decimalResult = String.Empty;
            string binaryResult = String.Empty;
            string hexadecimalResult = String.Empty;
            string decimalSeparator = $"{Separator(culture: Culture)}";
            string message = $"note, your decimal separator is a ' " + decimalSeparator + " '";

            if(String.IsNullOrWhiteSpace(Expression))
                return new CalcResult (decResult: decimalResult, binResult: binaryResult,
                hexResult: hexadecimalResult, message: message, separator: decimalSeparator);

            try
            {
                var myNode = CalculationParser.GrowNodeTree(input: Expression, culture: Culture);

                if (myNode != null)
                {
                    decimal result = myNode.Calculate();

                    decimalResult = result.ToString($"n{PrecisionForDecimalResult}", Culture).
                    TrimEnd('0').TrimEnd(Separator(culture: Culture));

                    int decSeparatorIndex = result.ToString().IndexOf(decimalSeparator);

                    if (decSeparatorIndex > 0 &&
                        result.ToString().Length - decSeparatorIndex > PrecisionForDecimalResult)

                        message = "decimal result is rounded, precision is " + PrecisionForDecimalResult;
                    
                    binaryResult = Convertions.ConvertDecimalToBinaryString
                        (input: result, roundingPrecisionForBinary: PrecisionForBinaryResult, culture: Culture);

                    int binSeparatorIndex = binaryResult.ToString().IndexOf(decimalSeparator);

                    if (binSeparatorIndex > 0 &&
                        binaryResult.ToString().Length - binSeparatorIndex > PrecisionForBinaryResult)

                        message = "binary result is rounded, precision is " + PrecisionForBinaryResult;

                    hexadecimalResult = Convertions.ConvertDecimalToHexadecimalString(input: result, culture: Culture);
                }
            }
            catch (CheckingException exception)
            {
                message = exception.Message;
            }
            catch (DivideByZeroException)
            {
                message = "Divide by Zero gives you infinity";
            }
            catch (OverflowException)
            {
                message = "value is too large or too small";
            }
            catch (Exception)
            {
                message = "Calculation failed";
            }

            return new CalcResult (decResult: decimalResult, binResult: binaryResult,
                hexResult: hexadecimalResult, message: message, separator: decimalSeparator);
        }
    }
}
