using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCalcMVC.Models;
using WebAppCalcMVC.ViewModels;

namespace WebAppCalcMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ILogger Log { get; }

        // get services via constructor
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> log)
        {
            // user management service (installed in Startup)
            _userManager = userManager;

            // signInManager service allows to authenticate the user and install or remove his cookies
            _signInManager = signInManager;

            Log = log;
        }


        // Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Log.LogInformation("\nRegister method was called:\n");

            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.Email};

                // add user to Db
                Log.LogInformation("\nAdd user to db:\n");

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

            Log.LogInformation("\nRegister method was completed:\n");

            return View(model);
        }

        // Login

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Log.LogInformation("\nLogin method was called:\n");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // check if the URL belongs to the application
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and / or password");
                }
            }

            Log.LogInformation("\nLogin method was completed:\n");

            return View(model);
        }

        // Log out

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // delete authentication cookies
            await _signInManager.SignOutAsync();

            Log.LogInformation("\nLogout method was called:\n");

            return RedirectToAction("Index", "Home");
        }
    }
}