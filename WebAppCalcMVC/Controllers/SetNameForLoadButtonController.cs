using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAppCalcMVC.Models;

namespace WebAppCalcMVC.Controllers
{
    public class SetNameForLoadButtonController : Controller
    {
        private ApplicationContext db;
        private ILogger Log { get; }

        public SetNameForLoadButtonController(ApplicationContext context, ILogger<SetNameForLoadButtonController> logger)
        {
            db = context;
            Log = logger;
        }

        [HttpPost]
        public ActionResult GetPhraseFromDb(string email)
        {
            Log.LogInformation("\nGetPhraseFromDb method was called:\n");
            Log.LogInformation("\nGet all savings of current user from db:\n");

            var savings = db.Savings
                .Where(s => s.User.Email == email)
                .ToList();

            var resultObject = new LoadButtonNameSetter
                (
                value_1: GetValue(identifier: 1, list: savings),
                value_2: GetValue(identifier: 2, list: savings),
                value_3: GetValue(identifier: 3, list: savings),
                value_4: GetValue(identifier: 4, list: savings),
                value_5: GetValue(identifier: 5, list: savings),
                message: null
                );

            var serializedOutput = JsonConvert.SerializeObject(resultObject);

            Log.LogInformation("\nGetPhraseFromDb method was completed\n");

            return Content(serializedOutput);
        }

        #region PrivateMethods 
        private string GetValue(int identifier, List<Saving> list)
        {
            if (list.Count() < identifier || String.IsNullOrWhiteSpace(list[identifier - 1].CalculationValue))

                return "empty";
            else
                return list[identifier - 1].CalculationValue;
        }
        #endregion
    }
}
