using Library.Data.Models_Entities_;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ApplicationUserController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var reuslt = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (reuslt.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid login, please try again!");

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
