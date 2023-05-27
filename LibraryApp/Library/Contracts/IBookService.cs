using Library.Data.Models_Entities_;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();
        
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task AddMovieAsync(AddBookViewModel model);

    }
}
