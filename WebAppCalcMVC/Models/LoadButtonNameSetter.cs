using Newtonsoft.Json;

namespace WebAppCalcMVC.Models
{
    public class LoadButtonNameSetter
    {
        internal LoadButtonNameSetter
            (string value_1, string value_2, string value_3, string value_4, string value_5, string message)
        {
            Value_1 = value_1;
            Value_2 = value_2;
            Value_3 = value_3;
            Value_4 = value_4;
            Value_5 = value_5;
            Message = message;
        }
        [JsonProperty]
        internal string Message { get; private set; }

        [JsonProperty]
        internal string Value_1 { get; private set; }

        [JsonProperty]
        internal string Value_2 { get; private set; }

        [JsonProperty]
        internal string Value_3{ get; private set; }

        [JsonProperty]
        internal string Value_4 { get; private set; }

        [JsonProperty]
        internal string Value_5 { get; private set; }
    }
}
