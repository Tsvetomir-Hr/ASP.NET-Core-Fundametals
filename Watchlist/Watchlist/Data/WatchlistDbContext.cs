using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Watchlist.Data
{
    public class WatchlistDbContext : IdentityDbContext<User>
    {
        public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
            : base(options)
        {
        }
        public DbSet<Genre> Genres { get; set; } = null!;

        public DbSet<Movie> Movies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserMovie>()
                .HasKey(x => new { x.UserId, x.MovieId });

            builder
                .Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 1,
                    Name = "Action"
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 3,
                    Name = "Drama"
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Romantic"
                });

            base.OnModelCreating(builder);
        }
    }
}