using Library.Data.Models_Entities_;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<MineBookViewModel>> GetMineBookAsync();


        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task AddMovieAsync(AddBookViewModel model);

        Task AddBookToCollectionAsync(int bookId,string userId);

        Task RemoveFromCollectionAsync(int bookId, string userId);

        
    }
}
