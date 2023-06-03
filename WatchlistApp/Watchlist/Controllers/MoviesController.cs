using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Contracts;
using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> All()
        {
            var models = await movieService.GetAllMoviesAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddMovieViewModel()
            {
                Genres = await movieService.GetAllGenresAsync()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            await movieService.AddMovieAsync(model);
            return RedirectToAction("All", "Movies");
        }
        [HttpPost]
        public async Task<IActionResult> AddToCollection(int movieId)
        {
            try
            {
                var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

                await movieService.AddToWatchedAsync(movieId, userId);

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Watched()
        {
            try
            {
                var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

                var models = await movieService.GetWatchedMoviesAsync(userId);

                return View(models);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }


        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            try
            {
                var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

                await movieService.RemoveFromWatchedAsync(movieId, userId);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            return RedirectToAction(nameof(Watched));
        }

    }
}
