using Microsoft.AspNetCore.Mvc;

namespace WebShopDemo.Controllers
{
    public class AccountController : Controller
    {
       
        public async Task<IActionResult> Register()
        {
            return View();
        }
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            return View();
        }
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            return View();
        }
    }
}
