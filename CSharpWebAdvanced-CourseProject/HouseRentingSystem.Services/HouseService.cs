
namespace HouseRentingSystem.Services
{
    using Microsoft.EntityFrameworkCore;

    using HouseRentingSystem.Services.Interfaces;
    using HouseRentingSystem.Web.ViewModels.Home;
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Web.ViewModels.House;
    using HouseRentingSystem.Data.Models;
    using HouseRentingSystem.Services.Data.Models.House;
    using HouseRentingSystem.Web.ViewModels.House.Enums;

    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext context;

        public HouseService(HouseRentingDbContext _context)
        {
            context = _context;
        }

        public async Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel)
        {
            IQueryable<House> houseQuery = context.Houses
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                houseQuery = houseQuery
                    .Where(h => h.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                houseQuery = houseQuery
                    .Where(h => EF.Functions.Like(h.Title, wildCard) ||
                    EF.Functions.Like(h.Address, wildCard) ||
                    EF.Functions.Like(h.Description, wildCard));
            }
            houseQuery = queryModel.HouseSorting switch
            {
                HouseSorting.Newest => houseQuery
                .OrderBy(h => h.CreatedOn),
                HouseSorting.Oldest => houseQuery
                .OrderByDescending(h => h.CreatedOn),
                HouseSorting.PriceAscending => houseQuery
                .OrderBy(h => h.PricePerMonth),
                HouseSorting.PriceDescending => houseQuery
                .OrderByDescending(h => h.PricePerMonth),
                _ => houseQuery.OrderBy(h => h.RenterId != null)
                .ThenByDescending(h => h.CreatedOn)
            };
            IEnumerable<HouseAllViewModel> allhouses = await houseQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.HousesPerPage)
                .Take(queryModel.HousesPerPage)
                .Select(h => new HouseAllViewModel()
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId.HasValue
                }).ToArrayAsync();

            int totalHouses = houseQuery.Count();

            return new AllHousesFilteredAndPagedServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = allhouses
            };
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
