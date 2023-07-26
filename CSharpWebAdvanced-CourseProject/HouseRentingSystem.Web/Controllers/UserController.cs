namespace HouseRentingSystem.Web.Controllers
{
    using HouseRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;

        public UserController(
            SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager,
            IUserStore<ApplicationUser> _userStore)
        {
            this.signInManager = _signInManager;
            this.userManager = _userManager;
            this.userStore = _userStore;
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
