using Watchlist.Web.Models;

namespace Watchlist.Services.Contracts
{
    public interface IMovieService
    {

        Task<IEnumerable<AllMoviesViewModel>> GetAllMovieAsync();
        Task<IEnumerable<GenreViewModel>> GetAllGenresAsync();

        Task AddMovieAsync(AddMovieViewModel model);

    }
}
