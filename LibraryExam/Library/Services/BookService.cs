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


        public async Task AddToMyCollectionAsync(string userId, AddBookViewModel book)
        {

            bool isAlreadyAdded = await context.IdentityUsersBooks.AnyAsync(b => b.BookId == book.Id && userId == b.CollectorId);
            if (!isAlreadyAdded)
            {
                IdentityUserBook book1 = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = book.Id

                };

                await context.IdentityUsersBooks.AddAsync(book1);

                await context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {

            return await context
                .Books
                .Select(b => new AllBookViewModel()
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

        public async Task<AddBookViewModel?> GetBookByIdAsync(int id)
        {
            return await context.Books
                .Where(b => b.Id == id)
                .Select(b => new AddBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Description = b.Description,
                    CategoryId = b.CategoryId
                }).FirstOrDefaultAsync();
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

        public async Task RemoveFromMyCollectionAsync(string userId, AddBookViewModel book)
        {
                var userBookToRemove = await context.IdentityUsersBooks
                    .FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

                if (userBookToRemove != null)
                {
                    context.IdentityUsersBooks.Remove(userBookToRemove);
                    await context.SaveChangesAsync();

                }
            
        }
    }
}
