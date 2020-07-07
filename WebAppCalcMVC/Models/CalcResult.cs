using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCalcMVC.Models
{
    public class CalcResult
    {
        internal CalcResult(string decResult, string binResult, string hexResult, string message, string separator)
        {
            DecimalResult = decResult;
            BinaryResult = binResult;
            HexadecimalResult = hexResult;
            Message = message;
            Separator = separator;
        }
        [JsonProperty]
        internal string DecimalResult { get; private set; }

        [JsonProperty]
        internal string BinaryResult { get; private set; }

        [JsonProperty]
        internal string HexadecimalResult { get; private set; }

        [JsonProperty]
        internal string Message { get; private set; }

        [JsonProperty]
        internal string Separator { get; private set; }
    }
}
