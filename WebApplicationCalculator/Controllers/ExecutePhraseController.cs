using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCalculator.Controllers
{
    public class ExecutePhraseController : Controller
    {
        // GET: ExecutePhrase
        public ActionResult Calc(string input)
        {
            return Content(input);
        }
    }
}