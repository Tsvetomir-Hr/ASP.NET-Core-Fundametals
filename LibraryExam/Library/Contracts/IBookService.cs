using Library.Data.Entities;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<MineBookViewModel>> GetMineBooksAsync(string userId);
        Task AddBookAsync(AddBookViewModel model);
        Task<IEnumerable<Category>> GetAllCategoriesForAddFormAsync();
        Task<AddBookViewModel?> GetBookByIdAsync(int id);
        Task AddToMyCollectionAsync(string userId,AddBookViewModel model);
        Task RemoveFromMyCollectionAsync(string userId,AddBookViewModel model);
    }
}
