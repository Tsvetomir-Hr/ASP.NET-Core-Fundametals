using Microsoft.EntityFrameworkCore;
using Watchlist.Data;
using Watchlist.Data.Models;
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

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var movie = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                GenreId = model.GenreId,
            };

            await context.AddAsync(movie);
            await context.SaveChangesAsync();
        }

        public async Task AddMovieToCollectionAsync(string userId, int id)
        {
            var user = await context.Users.FindAsync(userId);
            var movies = await context.Movies
                .Include(m => m.UsersMovies)
                .ToListAsync();
            bool isAlreadyAdded = movies.Any(m => m.UsersMovies.Any(um => um.UserId == userId && um.MovieId == id));
            if (!isAlreadyAdded)
            {
                var userMovie = new UserMovie()
                {
                    UserId = userId,
                    MovieId = id
                };
                await context.AddAsync(userMovie);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteMovieFromCollectionAsync(string userId, int id)
        {
            var user = await context.Users.FindAsync(userId);

            var movieToDelete = await context.UserMovie
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == id);

            if (movieToDelete != null)
            {
                context.UserMovie.Remove(movieToDelete);
                await context.SaveChangesAsync();

            }

        }

        public Task EditMovieAsync(AddMovieViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GenreViewModel>> GetAllGenresAsync()
        {
            return await context.Genres.Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = g.Name
            }).ToListAsync();
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

        public async Task<AddMovieViewModel?> GetMovieByIdAsync(int movieId)
        {
            return await context.Movies
                .Where(m=>m.Id==movieId)
                .Select(m => new AddMovieViewModel()
                {
                   Title = m.Title,
                   Director = m.Director,
                   Rating = m.Rating,
                   ImageUrl= m.ImageUrl,
                   GenreId = m.GenreId
                })
                .FirstOrDefaultAsync();
                

        }

        public async Task<IEnumerable<AllMoviesViewModel>> GetWatchedMoviesAsync(string userId)
        {
            return await context.Movies
                  .Include(m => m.UsersMovies)
                  .Where(m => m.UsersMovies.Any(um => um.UserId == userId))
                  .Select(m => new AllMoviesViewModel()
                  {
                      Id = m.Id,
                      Title = m.Title,
                      DIrector = m.Director,
                      Rating = m.Rating,
                      Genre = m.Genre.Name,
                      ImageUrl = m.ImageUrl
                  }).ToListAsync();

        }
    }
}
