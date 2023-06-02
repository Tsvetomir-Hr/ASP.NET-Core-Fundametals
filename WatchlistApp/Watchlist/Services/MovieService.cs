using Watchlist.Contracts;
using Watchlist.Data;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;
        public MovieService()
        {

        }
    }
}
