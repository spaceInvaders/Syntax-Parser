using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCalcMVC.Models;

namespace WebAppCalcMVC.Controllers
{
    public class HomeController : Controller
    {
        // number of saving slots for each logined user
        public static int NumberOfSaves { get; private set; } = 5;

        public IActionResult Index()
        {
            return View(NumberOfSaves);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
