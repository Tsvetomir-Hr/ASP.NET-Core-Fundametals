
using Library.Contracts;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService _bookService)
        {
            this.bookService = _bookService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var models = await bookService.GetAllBooksAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var models = await bookService.GetMineBookAsync();
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddBookViewModel()
            {
                Categories = await bookService.GetCategoriesAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await bookService.AddMovieAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went whrong!");

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int bookId)
        {

            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

                await bookService.AddBookToCollectionAsync(bookId, userId!);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction(nameof(All));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

            await bookService.RemoveFromCollectionAsync(bookId, userId);

            return RedirectToAction(nameof(Mine));
        }


    }
}
