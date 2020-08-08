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
        internal const int NumberOfSaves = 3;
        private ILogger Log { get; }

        public HomeController(ILogger<HomeController> log)
        {
            Log = log;
        }

        public IActionResult Index()
        {
            Log.LogInformation("\nIndex method was called\n");

            return View(NumberOfSaves);
        }

        public IActionResult Contact()
        {
            Log.LogInformation("\nContact method was called\n");

            return View();
        }
    }
}
