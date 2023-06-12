

using Watchlist.ViewModels;

namespace Watchlist.Services.Contracts
{
    public interface IMovieService
    {

        Task<IEnumerable<AllMoviesViewModel>> GetAllMovieAsync();
    }
}
