using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCalcMVC.Models;

namespace WebAppCalcMVC.ViewComponents
{
    public class NameOfSave_1 : ViewComponent
    {
        private ApplicationContext db;
        public NameOfSave_1(ApplicationContext context)
        {
            db = context;
        }
        public string Invoke(string email)
        {
            var savings = db.Savings
                .Where(s => s.User.Email == email)
                .ToList();

            if (savings.Count() == 0 || String.IsNullOrWhiteSpace(savings[0].CalculationValue))
                return "empty";
            else
                return savings[0].CalculationValue;
        }
    }

    public class NameOfSave_2 : ViewComponent
    {
        private ApplicationContext db;
        public NameOfSave_2(ApplicationContext context)
        {
            db = context;
        }
        public string Invoke(string email)
        {
            var savings = db.Savings
                .Where(s => s.User.Email == email)
                .ToList();

            if (savings.Count() <= 1 || String.IsNullOrWhiteSpace(savings[1].CalculationValue))
                return "empty";
            else
                return savings[1].CalculationValue;
        }
    }

    public class NameOfSave_3 : ViewComponent
    {
        private ApplicationContext db;
        public NameOfSave_3(ApplicationContext context)
        {
            db = context;
        }
        public string Invoke(string email)
        {
            var savings = db.Savings
                .Where(s => s.User.Email == email)
                .ToList();

            if (savings.Count() <= 2 || String.IsNullOrWhiteSpace(savings[2].CalculationValue))
                return "empty";
            else
                return savings[2].CalculationValue;
        }
    }

    public class NameOfSave_4 : ViewComponent
    {
        private ApplicationContext db;
        public NameOfSave_4(ApplicationContext context)
        {
            db = context;
        }
        public string Invoke(string email)
        {
            var savings = db.Savings
                .Where(s => s.User.Email == email)
                .ToList();

            if (savings.Count() <= 3 || String.IsNullOrWhiteSpace(savings[3].CalculationValue))
                return "empty";
            else
                return savings[3].CalculationValue;
        }
    }

    public class NameOfSave_5 : ViewComponent
    {
        private ApplicationContext db;
        public NameOfSave_5(ApplicationContext context)
        {
            db = context;
        }
        public string Invoke(string email)
        {
            var savings = db.Savings
                .Where(s => s.User.Email == email)
                .ToList();

            if (savings.Count() <= 4 || String.IsNullOrWhiteSpace(savings[4].CalculationValue))
                return "empty";
            else
                return savings[4].CalculationValue;
        }
    }
}
