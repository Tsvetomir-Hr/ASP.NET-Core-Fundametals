using Library.Contracts;
using Library.Data;
using Library.Data.Models_Entities_;
using Library.Models;
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
                .Include(b=>b.Category)
                .ToListAsync();

            return books.Select(book => new BookViewModel
            {
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
    }
}
