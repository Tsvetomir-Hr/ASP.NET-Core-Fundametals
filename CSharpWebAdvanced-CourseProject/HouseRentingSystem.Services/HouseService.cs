
namespace HouseRentingSystem.Services
{
    using Microsoft.EntityFrameworkCore;

    using HouseRentingSystem.Services.Interfaces;
    using HouseRentingSystem.Web.ViewModels.Home;
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Web.ViewModels.House;
    using HouseRentingSystem.Data.Models;

    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext context;

        public HouseService(HouseRentingDbContext _context)
        {
            context = _context;
        }

        public async Task CreateAsync(HouseFormModel formModel, string userId)
        {
            House house = new House()
            {
                Title = formModel.Title,
                Address = formModel.Address,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                PricePerMonth = formModel.PricePerMonth,
                CategoryId = formModel.CategoryId,
                AgentId = Guid.Parse(userId)

            };
            await context.Houses.AddAsync(house);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync()
        {

            return await context.Houses
                .OrderByDescending(h => h.CreatedOn)
                .Take(3)
                .Select(h => new IndexViewModel
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    ImageUrl = h.ImageUrl,
                })
                .ToArrayAsync();
        }
    }
}
