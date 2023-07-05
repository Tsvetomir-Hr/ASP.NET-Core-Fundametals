namespace HouseRentingSystem.Services.Interfaces
{
    using HouseRentingSystem.Web.ViewModels.House;
    using Web.ViewModels.Home;
    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();

        Task CreateAsync(HouseFormModel formmodel,string userId);
    }
}
