
namespace HouseRentingSystem.Services
{
    using Microsoft.EntityFrameworkCore;

    using HouseRentingSystem.Services.Interfaces;
    using HouseRentingSystem.Web.ViewModels.Home;
    using HouseRentingSystem.Data;

    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext context;

        public HouseService(HouseRentingDbContext _context)
        {
            context = _context;
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
