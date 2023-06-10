using Library.Data.Entities;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<MineBookViewModel>> GetMineBooksAsync(string userId);
        Task AddBookAsync(FormBookViewModel model);
        Task<IEnumerable<Category>> GetAllCategoriesForAddFormAsync();
        Task<FormBookViewModel?> GetBookByIdAsync(int id);
        Task AddToMyCollectionAsync(string userId,FormBookViewModel model);
        Task RemoveFromMyCollectionAsync(string userId,FormBookViewModel model);
        Task EditBookAsync(FormBookViewModel model, int id);
    }
}
