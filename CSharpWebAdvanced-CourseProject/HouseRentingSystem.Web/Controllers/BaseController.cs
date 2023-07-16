using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using static HouseRentingSystem.Common.NotificationMessageConstants;
namespace HouseRentingSystem.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        protected string? GetUserId()
        => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        protected IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later or contact administrator.";

            return RedirectToAction("Index", "Home");
        }
    }
}
