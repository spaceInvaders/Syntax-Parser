using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCalculator.Models
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
        public string BinaryResult { get; private set; }

        [JsonProperty]
        public string HexadecimalResult { get; private set; }

        [JsonProperty]
        public string Message { get; private set; }

        [JsonProperty]
        public string Separator { get; private set; }
    }
}
