namespace HouseRentingSystem.Web.Data
{


    using HouseRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using Microsoft.EntityFrameworkCore;
    public class HouseRentingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public HouseRentingDbContext(DbContextOptions<HouseRentingDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<House> Houses { get; set; } = null!;

        public DbSet<Agent> Agents { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
           
                

            base.OnModelCreating(builder);
        }

    }
}