using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Contracts
{
    public interface IBookService
    {

        public Task<IEnumerable<AllBooksViewModel>> GetAllAsync();

        public Task<IEnumerable<MineBookViewModel>> GetMyBooks(string userId);

        public Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();

        public Task AddBookAsync(FormBookViewModel model);

        public Task AddToCollectionAsync(string userId, int bookId);

        public Task<FormBookViewModel> GetBookById(int bookId);

        public Task RemoveFromCollectionAsync(string userId, int id);
    }
}
