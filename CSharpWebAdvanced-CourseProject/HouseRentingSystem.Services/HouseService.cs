
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
                .OrderByDescending(h => h.CreatedOn),
                HouseSorting.Oldest => houseQuery
                .OrderBy(h => h.CreatedOn),
                HouseSorting.PriceAscending => houseQuery
                .OrderBy(h => h.PricePerMonth),
                HouseSorting.PriceDescending => houseQuery
                .OrderByDescending(h => h.PricePerMonth),
                _ => houseQuery.OrderBy(h => h.RenterId != null)
                .ThenByDescending(h => h.CreatedOn)
            };
            IEnumerable<HouseAllViewModel> allhouses = await houseQuery
                .Where(h => h.isActive)
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

        public async Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<HouseAllViewModel> models = await context
                .Houses
                .Where(h => h.AgentId.ToString() == agentId && h.isActive)
                .Select(h => new HouseAllViewModel()
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId.HasValue
                }).ToArrayAsync();

            return models;

        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userid)
        {
            IEnumerable<HouseAllViewModel> models = await context
                .Houses
                .Where(h => h.RenterId.HasValue &&
                h.RenterId.ToString() == userid
                && h.isActive)
                .Select(h => new HouseAllViewModel()
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId.HasValue
                }).ToArrayAsync();

            return models;
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

        public async Task<bool> ExistById(string houseId)
        {
            return await context.Houses
                .Where(h => h.isActive)
                .AnyAsync(h => h.Id.ToString() == houseId);
        }

        public async Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId)
        {
            House house = await context
                .Houses
                .Include(h => h.Category)
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .Where(h => h.isActive)
                .FirstAsync(h => h.Id.ToString() == houseId);


            return new HouseDetailsViewModel
            {
                Id = house.Id.ToString(),
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                IsRented = house.RenterId.HasValue,
                Description = house.Description,
                Category = house.Category.Name,
                Agent = new Web.ViewModels.Agent.AgentInfoOnHouseViewModel()
                {
                    Email = house.Agent.User.Email,
                    PhoneNumber = house.Agent.User.PhoneNumber,
                }

            };

        }

        public async Task<HouseFormModel> GetHouseForEditAsync(string houseId)
        {
            House? house = await context
               .Houses
               .Include(h => h.Category)
               .Where(h => h.isActive)
               .FirstAsync(h => h.Id.ToString() == houseId);

            return new HouseFormModel
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = house.CategoryId
            };
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync()
        {

            return await context.Houses
                .OrderByDescending(h => h.CreatedOn)
                .Where(h => h.isActive)
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
