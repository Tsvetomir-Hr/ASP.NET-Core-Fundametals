﻿namespace HouseRentingSystem.Services.Interfaces
{
    using HouseRentingSystem.Services.Data.Models.House;
    using HouseRentingSystem.Web.ViewModels.House;

    using Web.ViewModels.Home;
    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();

        Task CreateAsync(HouseFormModel formmodel, string userId);

        Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);

        Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userid);

        Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId);

        Task<bool> ExistById(string houseId);

        Task<HouseFormModel> GetHouseForEditAsync();

    }
}
