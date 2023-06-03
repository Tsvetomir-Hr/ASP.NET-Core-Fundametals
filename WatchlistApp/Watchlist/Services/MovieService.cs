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


        public async Task AddToWatchedAsync(int movieId, string userId)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                throw new ArgumentException("Invalid movie ID!");
            }

            var user = await context.Users
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");
            }

            if (!user.UsersMovies.Any(u => u.MovieId == movieId))
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    Movie = movie,
                    MovieId = movieId,
                    UserId = userId,
                    User = user
                });

                await context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<MovieViewModel>> GetWatchedMoviesAsync(string userId)
        {
            var user = await context.Users
               .Include(u => u.UsersMovies)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");
            }
            var movies = await context.Movies
                .Include(u => u.UsersMovies)
                .Include(u => u.Genre)
                .ToListAsync();

            return movies.Where(m => m.UsersMovies.Any(um => um.UserId == userId))
                .Select(m => new MovieViewModel()
                {
                    Id = m.Id,
                    ImageUrl = m.ImageUrl,
                    Title = m.Title,
                    Director = m.Director,
                    Genre = m.Genre.Name,
                    Rating = m.Rating
                }).ToList();

        }

        public async Task RemoveFromWatchedAsync(int movieId, string userId)
        {


            var user = await context.Users
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");
            }
            var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid movie ID!");
            }

            user.UsersMovies.Remove(movie);


            await context.SaveChangesAsync();

        }
    }
}
