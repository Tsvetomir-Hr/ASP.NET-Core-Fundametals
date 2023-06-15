using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await bookService.GetAllAsync();
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var models = await bookService.GetMyBooks(GetUserId()!);
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var model = new FormBookViewModel()
            {
                Categories = await bookService.GetAllCategoriesAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FormBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await bookService.AddBookAsync(model);

            return RedirectToAction(nameof(All));
        }
        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            string userId = GetUserId();
            var book = await bookService.GetBookById(id);

            if (userId != null && book != null)
            {
                await bookService.AddToCollectionAsync(userId, id);
            }
            return RedirectToAction(nameof(All));

        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            string userId = GetUserId();
            var book = await bookService.GetBookById(id);

            if (userId != null && book != null)
            {
                await bookService.RemoveFromCollectionAsync(userId, id);
            }
            return RedirectToAction(nameof(Mine));

        }

    }
}
