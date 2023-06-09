using Library.Data.Entities;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<MineBookViewModel>> GetMineBooksAsync(string userId);
        Task AddBookAsync(AddBookViewModel model);
        Task<IEnumerable<Category>> GetAllCategoriesForAddFormAsync();
        Task AddToMyCollectionAsync(string userId,int bookId);
    }
}
