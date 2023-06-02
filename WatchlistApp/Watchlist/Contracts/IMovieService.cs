using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync();

        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task AddMovieAsync(AddMovieViewModel model);
    }
}
