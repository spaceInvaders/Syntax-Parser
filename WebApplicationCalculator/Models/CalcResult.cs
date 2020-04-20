using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCalculator.Models
{
    public class CalcResult
    {
        public CalcResult(string decResult, string binResult, string hexResult, string message)
        {
            DecimalResult = decResult;
            BinaryResult = binResult;
            HexadecimalResult = hexResult;
            Message = message;
        }
        public string DecimalResult { get; private set; }
        public string BinaryResult { get; private set; }
        public string HexadecimalResult { get; private set; }
        public string Message { get; private set; }
    }
}
