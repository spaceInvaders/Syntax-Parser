﻿using System;
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

            var user = GetCurrentUserFromb(input: inputObject);

            if (CheckIfSavingSlotsAreFinallyFilled(user: user, out int numberOfRowsToRemove))
            {
                var savingsToRemove = db.Savings
                    .Where(s => s.User == user)
                    .Take(numberOfRowsToRemove);

                db.Savings.RemoveRange(savingsToRemove);
                db.SaveChanges();
            }

            var newSaving = CreateNewSaving(user: user, inputObject: inputObject);

            db.Savings.Add(newSaving);
            db.SaveChanges();

            // get all savings of current user from db

            var savings = db.Savings
                .Where(s => s.User.Email == inputObject.Mail)
                .ToList();

            // get last saving

            var phraseFromDb = savings.Last().CalculationValue;

            var message = $"Your phrase '{phraseFromDb}' was successfully saved!";

            // initializing an object with all savings and message for output with id 'result_notifier'
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
        private User GetCurrentUserFromb(PhraseWithMailToSave input)
        {
            return db.Users
                .FirstOrDefault(u => u.Email == input.Mail);
        }

        private bool CheckIfSavingSlotsAreFinallyFilled(User user, out int numberOfRowsToRemove)
        {
            var quantityOfSavesOfCurrentUser = db.Savings
                .Where(s => s.UserId == user.Id)
                .Count();

            numberOfRowsToRemove = quantityOfSavesOfCurrentUser - HomeController.NumberOfSaves + 1;

            return quantityOfSavesOfCurrentUser >= HomeController.NumberOfSaves;
        }

        private Saving CreateNewSaving(User user, PhraseWithMailToSave inputObject)
        {
            var dateOfSave = DateTime.Now;

            var saving = new Saving()
            {
                CalculationValue = RemoveWhiteSpaces(input: inputObject.PhraseToSave),
                DateOnServer = dateOfSave,
                DateOnClient = inputObject.DateOnClient,
                UserId = user.Id
            };

            return saving;
        }

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
