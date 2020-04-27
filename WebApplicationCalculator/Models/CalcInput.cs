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
        private const int PrecisionForDecimalResult = 10;
        private const int PrecisionForBinaryResult = 30;
        private const string Infinity = "\u221E";

        [JsonConstructor]
        internal CalcInput(string expression, CultureInfo culture)
        {
            Expression = expression;
            Culture = culture;
        }

        internal string Expression { get; private set; }
        internal CultureInfo Culture { get; private set; }

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

                    binaryResult = Convertions.ConvertDecimalToBinaryString
                        (input: Decimal.Parse(decimalResult, Culture), precisionForBinary: PrecisionForBinaryResult,
                        culture: Culture);

                    hexadecimalResult = Convertions.ConvertDecimalToHexadecimalString
                        (input: Decimal.Parse(decimalResult, Culture));

                    int signsAfterSeparator = SignsAfterSeparator(number: result, separator: decimalSeparator);

                    message = CheckOnFractionalRounding(afterRounding: decimalResult, precision: PrecisionForDecimalResult,
                        message: message, beforeRounding: result.ToString($"n{signsAfterSeparator}", Culture).
                        TrimEnd('0').TrimEnd(Separator(culture: Culture)));
                }
            }
            catch (CheckingException exception)
            {
                message = exception.Message;
            }
            catch (DivideByZeroException)
            {
                message = "Divide by Zero gives you infinity";
                decimalResult = Infinity;
                binaryResult = Infinity;
                hexadecimalResult = Infinity;
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

        #region PrivateMethods

        private char Separator(CultureInfo culture) => Convert.ToChar(culture.NumberFormat.NumberDecimalSeparator);

        private string CheckOnFractionalRounding
            (string message, string beforeRounding, string afterRounding, int precision)
        {
            if (beforeRounding != afterRounding)
                
                return "result is rounded, precision is " + precision + " signs after decimal separator";
            else
                return message;
        }

        private int SignsAfterSeparator(decimal number, string separator)
        {
            int separatorIndex = number.ToString().IndexOf(separator);

            return number.ToString().Length - separatorIndex;
        }

        #endregion
    }
}
