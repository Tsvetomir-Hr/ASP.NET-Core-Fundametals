using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var entity = new Movie()
            {
                Director = model.Director,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                Title = model.Title
            };

            await context.Movies.AddAsync(entity);
            await context.SaveChangesAsync();

        }

        public async Task AddMovieToCollectionASync(int movieId, string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");

            }
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                throw new ArgumentException("Invalid movie ID!");
            }

            user.UsersMovies.Add(new UserMovie()
            {
                MovieId = movieId,
                UserId = userId,
                Movie = movie,
                User = user
            });
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var entities = await context.Movies
                .Include(m => m.Genre)
                .ToListAsync();

            return entities
             .Select(m => new MovieViewModel()
             {
                 Director = m.Director,
                 Title = m.Title,
                 Genre = m?.Genre?.Name,
                 ImageUrl = m.ImageUrl,
                 Rating = m.Rating

             });

        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }
    }
}
