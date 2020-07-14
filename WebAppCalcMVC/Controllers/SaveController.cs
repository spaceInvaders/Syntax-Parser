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
            var inputObject = JsonConvert.DeserializeObject<PhraseWithMailToSave>(serializedInput);

            var user = db.Users.Where(u => u.Email == inputObject.Mail);

            var saving = new Saving()
            {
                CalculationValue = inputObject.PhraseToSave,
                Date = DateTime.Now,
                User = user;
             }




            var phraseFromDb = "";

            return Content($"Your phrase '{phraseFromDb}' was successfully saved!");
        }
    }
}
