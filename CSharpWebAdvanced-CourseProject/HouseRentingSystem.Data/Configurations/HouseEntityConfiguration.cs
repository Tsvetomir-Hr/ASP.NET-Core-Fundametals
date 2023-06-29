using HouseRentingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading;

namespace HouseRentingSystem.Data.Configurations
{
    public class HouseEntityConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {

            builder
                .Property(h => h.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            builder.HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Renter)
                .WithMany(r => r.RentedHouses)
                .HasForeignKey(h => h.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateHouses());

        }

        private House[] GenerateHouses()
        {
            ICollection<House> houses = new HashSet<House>();

            House house;

            house = new House()
            {
                Title = "Big House Marina",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://cdn.remax.co.za/listings/4007973/original/cd80cc39-4125-d73e-754c-c8288c962867.jpg",
                PricePerMonth = 2100.00M,
                CategoryId = 3,
                AgentId = Guid.Parse("5BAFAE9D-FDFD-45CB-98B5-9981B0E408CA"),
                RenterId = Guid.Parse("E68C56FB-6DB2-4C11-62ED-08DB7879BC1D")
            };

            houses.Add(house);

            house = new House()
            {
                Title = "Family House Comfort",
                Address = "Near the Sea Garden in Burgas, Bulgaria",
                Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                PricePerMonth = 1200.00M,
                CategoryId = 2,
                AgentId = Guid.Parse("5BAFAE9D-FDFD-45CB-98B5-9981B0E408CA")
            };
            houses.Add(house);

            house = new House()
            {
                Title = "Grand House",
                Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                Description = "This luxurious house is everything you will need. It is just excellent.",
                ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
                PricePerMonth = 2000.00M,
                CategoryId = 2,
                AgentId = Guid.Parse("5BAFAE9D-FDFD-45CB-98B5-9981B0E408CA")
            };
            houses.Add(house);

            return houses.ToArray();
        }
    }
}
