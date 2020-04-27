using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CalculatorCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCalculator.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebApplicationCalculator.Controllers
{
    public class ExecutePhraseController : Controller
    {
        [HttpGet]
        public ActionResult GetResult(string serializedInput)
        {
            var inputObject = JsonConvert.DeserializeObject<CalcInput>(serializedInput);
            var resultObject = inputObject.ExecuteExpression();
            var serializedOutput = JsonConvert.SerializeObject(resultObject);

            return Content(serializedOutput);
        }

        [HttpPost]
        public ActionResult GetMessage(string culture)
        {
            var inputObject = new CalcInput(expression: string.Empty, culture: new CultureInfo(culture));
            var resultObject = inputObject.ExecuteExpression();
            var example = $"7{resultObject.Separator}3 + p^(8{resultObject.Separator}4)";

            return Content($"Hi! Type smth like this: " + example);
        }
    }
}