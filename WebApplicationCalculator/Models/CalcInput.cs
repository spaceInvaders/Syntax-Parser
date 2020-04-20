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
            string decimalResult = null;
            string binaryResult = null;
            string hexadecimalResult = null;
            string message = null;

            try
            {
                var myNode = CalculationParser.GrowNodeTree(input: Expression, culture: Culture);

                if (myNode != null)
                {
                    decimal result = myNode.Calculate();

                    int separatorIndex = result.ToString().IndexOf(Separator(culture: Culture));

                    if (separatorIndex > 0 && result.ToString().Length - separatorIndex > PrecisionForDecimalResult)
                    {
                        message = "decimal result is rounded, precision is" + PrecisionForDecimalResult;
                    }

                    decimalResult = result.ToString($"n{PrecisionForDecimalResult}", Culture).
                    TrimEnd('0').TrimEnd(Separator(culture: Culture));

                    binaryResult = Convertions.ConvertDecimalToBinaryString
                        (input: result, roundingPrecisionForBinary: PrecisionForBinaryResult, culture: Culture);
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

            return new CalcResult
                (decResult: decimalResult, binResult: binaryResult, hexResult: hexadecimalResult, message: message);
        }
    }
}
