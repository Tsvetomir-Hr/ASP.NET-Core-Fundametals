using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync();

        Task<IEnumerable<MovieViewModel>> GetWatchedMoviesAsync(string userId);

        Task<IEnumerable<Genre>> GetAllGenresAsync();

        Task AddMovieAsync(AddMovieViewModel model);


        Task AddToWatchedAsync(int movieId, string userId);

        Task RemoveFromWatchedAsync(int movieId, string userId);

    }
}
