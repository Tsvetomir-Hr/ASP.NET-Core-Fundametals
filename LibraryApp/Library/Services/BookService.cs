using Library.Contracts;
using Library.Data;
using Library.Data.Models_Entities_;
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

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");
            }
            var book = await context.Books.FindAsync(bookId);
            if (book == null)
            {
                throw new ArgumentException("Invalid book ID!");
            }
            if (user.ApplicationUsersBooks.Any(b => b.BookId == bookId))
            {
                return;
            }

            user.ApplicationUsersBooks.Add(new ApplicationUserBook()
            {
                ApplicationUserId = userId,
                ApplicationUser = user,
                BookId = bookId,
                Book = book

            });

            await context.SaveChangesAsync();
        }

        public async Task AddMovieAsync(AddBookViewModel model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            var books = await context.Books
                .Include(b => b.Category)
                .ToListAsync();

            return books.Select(book => new BookViewModel
            {
                Id = book.Id,
                ImageUrl = book.ImageUrl,
                Title = book.Title,
                Author = book.Author,
                Rating = book.Rating,
                Category = book.Category.Name!
            });
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<MineBookViewModel>> GetMineBookAsync()
        {
            var books = await context.Books
                .Include(b => b.Category)
                .Include(b => b.ApplicationUsersBooks)
                .ToListAsync();
            return books
                .Where(b => b.ApplicationUsersBooks.Any(au => au.BookId == b.Id))
                .Select(book => new MineBookViewModel
                {
                    Id = book.Id,
                    ImageUrl = book.ImageUrl,
                    Title = book.Title,
                    Author = book.Author,
                    Category = book.Category.Name!
                });
        }


        public async Task RemoveFromCollectionAsync(int bookId, string userId)
        {
            var user = context.Users.Find(userId);

            var minebooks = await GetMineBookAsync();

            if (minebooks.Any(b => b.Id == bookId))
            {
               
                    
                
            }




            //user.ApplicationUsersBooks.Remove();


        }
    }
}
