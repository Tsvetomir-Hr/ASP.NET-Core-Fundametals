using Microsoft.AspNetCore.Mvc;

namespace Watchlist.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
