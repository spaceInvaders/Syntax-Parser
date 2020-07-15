using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppCalcMVC.Models;

namespace WebAppCalcMVC.Controllers
{
    public class SaveController : Controller
    {
        private ApplicationContext db;
        public SaveController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public ActionResult SaveToDb(string serializedInput)
        {
            var inputObject = JsonConvert
                .DeserializeObject<PhraseWithMailToSave>(serializedInput);

            var user = db.Users
                .FirstOrDefault(u => u.Email == inputObject.Mail);

            var dateOfSave = DateTime.Now;

            var saving = new Saving()
            {
                CalculationValue = inputObject.PhraseToSave,
                DateOnServer = dateOfSave,
                DateOnClient = inputObject.DateOnClient,
                UserId = user.Id
            };

            db.Savings.Add(saving);
            db.SaveChanges();

            var phraseFromDb = db.Savings
                .FirstOrDefault(p => p.DateOnServer == dateOfSave && p.UserId == user.Id)
                .CalculationValue;

            return Content($"Your phrase '{phraseFromDb}' was successfully saved!");
        }
    }
}
