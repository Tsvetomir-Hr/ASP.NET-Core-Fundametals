using Library.Contracts;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var books = await bookService.GetAllBooksAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {

            var userId = GetUserId();

            var books = await bookService.GetMineBooksAsync(userId);

            return View(books);


        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await bookService.GetAllCategoriesForAddFormAsync();

            var model = new AddBookViewModel()
            {
                Categories = categories
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
            await bookService.AddBookAsync(model);
            return RedirectToAction("All", "Book");
        }


        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await bookService.AddToMyCollectionAsync(userId, book);

            return RedirectToAction(nameof(All));


        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Mine));
            }
            var userId = GetUserId();

            await bookService.RemoveFromMyCollectionAsync(userId, book);

            return RedirectToAction(nameof(Mine));

        }

    }
}
