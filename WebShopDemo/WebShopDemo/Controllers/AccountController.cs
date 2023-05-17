using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShopDemo.Core.Data.Models.Account;
using WebShopDemo.Models;

namespace WebShopDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;

        }
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var user = new ApplicationUser
            {
                Email = model.Email,
                FirstName = model.FirstName,
                EmailConfirmed = true,
                LastName = model.LastName,
                UserName = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(null, "Something went wrong!");
            return View(model);
        }
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            return View();
        }
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            return View();
        }
    }
}
