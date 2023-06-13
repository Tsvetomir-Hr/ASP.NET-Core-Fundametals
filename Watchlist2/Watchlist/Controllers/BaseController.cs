using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Watchlist.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       protected string GetUserIdAsync()
        {
            return User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;
        }
    }
}
