using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;
        public MovieService(WatchlistDbContext context)
        {
            this.context = context;
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var movie = new Movie()
            {
                Id = model.Id,
                Title = model.Title,
                Director = model.Director,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                GenreId = model.GenreId
            };

            await context.AddAsync(movie);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync()
        {
            var movies = await context.Movies
                .Include(m => m.Genre)
                .ToArrayAsync();

            return movies.Select(m => new MovieViewModel()
            {
                ImageUrl = m.ImageUrl,
                Title = m.Title,
                Director = m.Director,
                Rating = m.Rating,
                Id = m.Id,
                Genre = m.Genre.Name?.ToString() ?? string.Empty
            });
        }
    }
}
