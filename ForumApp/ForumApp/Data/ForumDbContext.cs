using ForumApp.Data.Configure;
using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data
{
    public class ForumDbContext : DbContext
    {

        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            modelBuilder.Entity<Post>()
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
