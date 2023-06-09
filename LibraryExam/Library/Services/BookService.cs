using Library.Contracts;
using Library.Data;
using Library.Data.Entities;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;
        public BookService(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            var book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId,

            };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task AddToMyCollectionAsync(string userId, int bookId)
        {

            var user = await context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid User ID!");
            }
            var book = await context.Books.FindAsync(bookId);
            if (book == null)
            {
                throw new ArgumentException("Invalid Book ID!");
            }
            bool isAlreadyAdded = await context.IdentityUsersBooks.AnyAsync(b => b.BookId == bookId && user == b.Collector);
            if (!isAlreadyAdded)
            {
                IdentityUserBook book1 = new IdentityUserBook()
                {
                    CollectorId = userId,
                    Collector = user,
                    BookId = bookId,
                    Book = book
                };

                await context.IdentityUsersBooks.AddAsync(book1);

                await context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {

            return await context
                .Books
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Category = b.Category.Name,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                }).ToListAsync();

        }

        public async Task<IEnumerable<Category>> GetAllCategoriesForAddFormAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<MineBookViewModel>> GetMineBooksAsync(string userId)
        {
            return await context.Books
                .Include(b => b.IdentityUserBook)
                .Where(b => b.IdentityUserBook.Any(b => b.CollectorId == userId))
                .Select(b => new MineBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Category = b.Category.Name,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description
                }).ToListAsync();

        }
    }
}
