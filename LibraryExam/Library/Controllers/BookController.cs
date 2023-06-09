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
            try
            {
                var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var books = await bookService.GetMineBooksAsync(userId);

                return View(books);
            }
            catch (Exception)
            {

                throw;
            }

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
        public async Task<IActionResult> AddToCollection(int bookId)
        {
            await bookService.AddToMyCollectionAsync(GetUserId(), bookId);

            return RedirectToAction(nameof(All));
        }

    }
}
