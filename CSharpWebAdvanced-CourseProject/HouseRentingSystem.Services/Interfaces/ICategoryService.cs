namespace HouseRentingSystem.Services.Interfaces
{
    using HouseRentingSystem.Web.ViewModels.Category;

    public interface ICategoryService
    {

        Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync();
    }
}
