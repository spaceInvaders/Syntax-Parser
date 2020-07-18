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

            // get (Read) current user from db

            var user = db.Users
                .FirstOrDefault(u => u.Email == inputObject.Mail);

            // if User has 5 savings, remove the oldest one from db 

            var quantityOfSavesOfCurrentUser = db.Savings
                .Where(s => s.UserId == user.Id)
                .Count();
            
            if (quantityOfSavesOfCurrentUser >= 5)
            {
                var minId = db.Savings
                    .Where(s => s.User == user)
                    .Min(s => s.Id);

                var oldestSave = db.Savings.Find(minId);

                db.Savings.Remove(oldestSave);
                db.SaveChanges();
            }

            // create new saving and push it to db

            var dateOfSave = DateTime.Now;

            var saving = new Saving()
            {
                CalculationValue = RemoveWhiteSpaces(input: inputObject.PhraseToSave),
                DateOnServer = dateOfSave,
                DateOnClient = inputObject.DateOnClient,
                UserId = user.Id
            };

            db.Savings.Add(saving);
            db.SaveChanges();

            // get recently saved culcation phrase from db and send it to client

            var phraseFromDb = db.Savings
                .FirstOrDefault(p => p.DateOnServer == dateOfSave && p.UserId == user.Id)
                .CalculationValue;

            return Content($"Your phrase '{phraseFromDb}' was successfully saved!");
        }

        #region PrivateMethods
        private static string RemoveWhiteSpaces(string input)
        {
            return new string(input.ToCharArray().Where(character => !Char.IsWhiteSpace(character)).ToArray());
        }
        #endregion
    }
}
