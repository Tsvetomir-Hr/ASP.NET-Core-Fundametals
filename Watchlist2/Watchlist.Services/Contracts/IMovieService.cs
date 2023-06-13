using Watchlist.Web.Models;

namespace Watchlist.Services.Contracts
{
    public interface IMovieService
    {

        Task<IEnumerable<AllMoviesViewModel>> GetAllMovieAsync();
        Task<IEnumerable<AllMoviesViewModel>> GetWatchedMoviesAsync(string userId);


        Task<IEnumerable<GenreViewModel>> GetAllGenresAsync();

        Task AddMovieAsync(AddMovieViewModel model);

        Task AddMovieToCollectionAsync(string userId, int id);

        Task DeleteMovieFromCollectionAsync(string userId, int id);

        Task EditMovieAsync(AddMovieViewModel model);

        Task<AddMovieViewModel> GetMovieByIdAsync(int movieId);

    }
}
