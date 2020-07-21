using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            // get current user from db
            var user = db.Users
                .FirstOrDefault(u => u.Email == inputObject.Mail);

            // if user has 5 savings, remove the oldest one from db 
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

            // get all savings of current user from db

            var savings = db.Savings
                .Where(s => s.User.Email == inputObject.Mail)
                .ToList();

            // get last saving

            var phraseFromDb = savings.Last().CalculationValue;

            var message = $"Your phrase '{phraseFromDb}' was successfully saved!";

            var resultObject = new LoadButtonNameSetter
                (
                value_1: GetValue(identifier: 1, list: savings),
                value_2: GetValue(identifier: 2, list: savings),
                value_3: GetValue(identifier: 3, list: savings),
                value_4: GetValue(identifier: 4, list: savings),
                value_5: GetValue(identifier: 5, list: savings),
                message: message
                );

            var serializedOutput = JsonConvert.SerializeObject(resultObject);

            return Content(serializedOutput);
        }

        #region PrivateMethods
        private static string RemoveWhiteSpaces(string input)
        {
            return new string(input.ToCharArray().Where(character => !Char.IsWhiteSpace(character)).ToArray());
        }

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
