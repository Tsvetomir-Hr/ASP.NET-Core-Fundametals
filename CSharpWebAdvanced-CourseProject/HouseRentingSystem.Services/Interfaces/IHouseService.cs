namespace HouseRentingSystem.Services.Interfaces
{
    using HouseRentingSystem.Services.Data.Models.House;
    using HouseRentingSystem.Services.Data.Models.Statistics;
    using HouseRentingSystem.Web.ViewModels.House;

    using Web.ViewModels.Home;
    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();

        Task<string> CreateAsync(HouseFormModel formmodel, string userId);

        Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);

        Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userid);

        Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId);

        Task<bool> ExistByIdAsync(string houseId);

        Task<HouseFormModel> GetHouseForEditAsync(string houseId);

        Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId);

        Task EditHouseByIdAndFormModel(string houseId, HouseFormModel model);

        Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId);

        Task DeleteHouseByIdAsync(string houseId);

        Task<bool> isRentedByIdAsync(string houseId);

        Task RentHouseAsync(string houseId, string userId);

        Task<bool> IsRenterByUserWithIdAsync(string houseId, string userid);

        Task LeaveHouseAsync(string houseId);

        Task<StatisticsServiceModel> GetStatisticsAsync();


    }
}
