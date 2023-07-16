namespace HouseRentingSystem.Services
{
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Services.Interfaces;
    using HouseRentingSystem.Web.ViewModels.Category;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly HouseRentingDbContext context;

        public CategoryService(HouseRentingDbContext _context)
        {
            this.context = _context;
        }
        public async Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync()
        {
            return await context.Categories
                 .AsNoTracking()
                 .Select(c => new HouseSelectCategoryFormModel()
                 {
                     Id = c.Id,
                     Name = c.Name,
                 })
                 .ToArrayAsync();
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            return await context.Categories
                 .Select(c => c.Name)
                 .ToArrayAsync();

            
        }
        public async Task<bool> ExistByIdAsync(int id)
        {
            return await context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
