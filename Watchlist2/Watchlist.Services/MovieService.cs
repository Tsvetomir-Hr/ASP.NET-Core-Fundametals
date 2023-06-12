using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<AllMoviesViewModel>> GetAllMovieAsync()
        {
            return await context.Movies.Select(m => new AllMoviesViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                DIrector = m.Director,
                Rating = m.Rating,
                Genre = m.Genre.Name,
                ImageUrl = m.ImageUrl
            })
                 .ToListAsync();

        }
    }
}
