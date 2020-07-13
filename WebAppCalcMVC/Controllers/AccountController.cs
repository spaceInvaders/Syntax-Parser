using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppCalcMVC.Models;
using WebAppCalcMVC.ViewModels;

namespace WebAppCalcMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // get services via constructor
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            // user management service (installed in Startup)
            _userManager = userManager;

            // signInManager service allows to authenticate the user and install or remove his cookies
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.Email};

                // add user to Db
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // instal cookies for user, that has been added to Db
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}