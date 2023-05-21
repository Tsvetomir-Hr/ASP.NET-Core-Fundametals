using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Web.Data.Entities;

namespace TaskBoardApp.Web.Data
{
    /// <summary>
    /// App db context
    /// </summary>
    public class TaskBoardAppDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Constructor for configuring the dbContext
        /// </summary>
        /// <param name="options"></param>
        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {
           
        }
        /// <summary>
        /// Db set table for boards entities
        /// </summary>
        public DbSet<Board> Boards { get; set; } = null!;
        /// <summary>
        /// Db set table for task entites
        /// </summary>
        public DbSet<Entities.Task> Tasks { get; set; } = null!;

        /// <summary>
        /// Method for FluentApi configuration to the database
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Entities.Task>()
                .HasOne(t => t.Board)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t=>t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);
           

            base.OnModelCreating(builder);
        }

    }
}