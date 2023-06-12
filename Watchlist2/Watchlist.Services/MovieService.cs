using Watchlist.Data;
using Watchlist.Services.Contracts;
using Watchlist.Web.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;
        public MovieService(WatchlistDbContext context)
        {
            this.context = context;
        }
        public Task<IEnumerable<AllMoviesViewModel>> GetAllMovieAsync()
        {

        }
    }
}
