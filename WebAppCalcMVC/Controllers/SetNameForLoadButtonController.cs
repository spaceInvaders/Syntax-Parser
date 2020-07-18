using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppCalcMVC.Models;

namespace WebAppCalcMVC.Controllers
{
    public class SetNameForLoadButtonController : Controller
    {
        private ApplicationContext db;
        public SetNameForLoadButtonController(ApplicationContext context)
        {
            db = context;
        }
        [HttpPost]
        public ActionResult GetPhraseFromDb(string email)
        {
            var savings = db.Savings
                .Where(s => s.User.Email == email)
                .ToList();

            var resultObject = new LoadButtonNameSetter
                (
                value_1: GetValue(identifier: 1, email: email, list: savings),
                value_2: GetValue(identifier: 2, email: email, list: savings),
                value_3: GetValue(identifier: 3, email: email, list: savings),
                value_4: GetValue(identifier: 4, email: email, list: savings),
                value_5: GetValue(identifier: 5, email: email, list: savings)
                );

            var serializedOutput = JsonConvert.SerializeObject(resultObject);

            return Content(serializedOutput);
        }

        #region PrivateMethods
        private string GetValue(int identifier, string email, List<Saving> list)
        {
            if (list.Count() < identifier || String.IsNullOrWhiteSpace(list[identifier - 1].CalculationValue))

                return "empty";
            else
                return list[identifier - 1].CalculationValue;
        }
        #endregion
    }
}
