using HouseRentingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            categories.Add(new Category
            {
                Id = 1,
                Name = "Cottage",
            });

            categories.Add(new Category
            {
                Id = 2,
                Name = "Single-Family"
            });
            categories.Add(new Category
            {
                Id = 3,
                Name = "Duplex"
            });

            return categories.ToArray();
        }
    }
}
