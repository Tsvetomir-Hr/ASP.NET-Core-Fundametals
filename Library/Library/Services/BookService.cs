using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;
        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task AddBookAsync(FormBookViewModel model)
        {

            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                ImageUrl = model.Url,
                Description = model.Description,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };

            await dbContext.Books.AddAsync(book);

            await dbContext.SaveChangesAsync();


        }

        public async Task AddToCollectionAsync(string userId, int bookId)
        {
            bool isAlreadyAdded = await dbContext.UsersBooks.AnyAsync(ub => ub.CollectorId == userId && ub.BookId == bookId);

            if (!isAlreadyAdded)
            {
                var book = await dbContext.Books.FindAsync(bookId);

                book!.UsersBooks.Add(new IdentityUserBook()
                {
                    BookId = bookId,
                    CollectorId = userId,
                });
                await dbContext.SaveChangesAsync();
            }


        }

        public async Task<IEnumerable<AllBooksViewModel>> GetAllAsync()
        {
            return await dbContext.Books
                .Select(b => new AllBooksViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    Author = b.Author,
                    Rating = b.Rating,
                    Category = b.Category.Name
                }).ToListAsync();

        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return await dbContext
                .Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<FormBookViewModel?> GetBookById(int bookId)
        {
            return await dbContext
                .Books
                .Where(b => b.Id == bookId)
                .Select(b => new FormBookViewModel()
                {
                    Title = b.Title,
                    Author = b.Author,
                    Url = b.ImageUrl,
                    Description = b.Description,
                    Rating = b.Rating,
                    CategoryId = b.CategoryId,
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MineBookViewModel>> GetMyBooks(string userId)
        {
            return await dbContext.Books
                .Where(b => b.UsersBooks.Any(ub => ub.CollectorId == userId))
                .Select(b => new MineBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    Author = b.Author,
                    Description = b.Description,
                    Category = b.Category.Name
                }).ToListAsync();

        }

        public async Task RemoveFromCollectionAsync(string userId, int id)
        {
            var book = await dbContext.UsersBooks.FirstAsync(ub => ub.CollectorId == userId && ub.BookId == id);

            if (book != null)
            {

                dbContext.UsersBooks.Remove(book);

                await dbContext.SaveChangesAsync();

            }
        }
    }
}
