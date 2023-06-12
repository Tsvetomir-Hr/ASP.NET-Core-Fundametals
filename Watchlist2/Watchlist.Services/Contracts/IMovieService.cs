using Watchlist.Web.Models;

namespace Watchlist.Services.Contracts
{
    public interface IMovieService
    {

        Task<IEnumerable<AllMoviesViewModel>> GetAllMovieAsync();
    }
}
