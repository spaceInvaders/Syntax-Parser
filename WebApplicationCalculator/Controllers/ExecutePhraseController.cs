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

namespace WebApplicationCalculator.Controllers
{
    public class ExecutePhraseController : Controller
    {
        public ActionResult GetResult(string serializedInput)
        {
            var inputObject = JsonConvert.DeserializeObject<CalcInput>(serializedInput);

            CalcResult resultObject = inputObject.ExecuteExpression();

            string serializedOutput = JsonConvert.SerializeObject(resultObject);

            return Content(serializedOutput);
        }
    }
}