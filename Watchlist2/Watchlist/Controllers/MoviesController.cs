using Microsoft.AspNetCore.Mvc;
using Watchlist.Services.Contracts;

namespace Watchlist.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly IMovieService movieService;
        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> All()
        {
            var models = await movieService.GetAllMovieAsync();
            return View(models);
        }
    }
}
