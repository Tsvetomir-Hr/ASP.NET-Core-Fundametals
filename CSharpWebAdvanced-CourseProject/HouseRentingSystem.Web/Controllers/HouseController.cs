
namespace HouseRentingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using HouseRentingSystem.Services.Interfaces;
    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;
        public HouseController(IHouseService _houseService)
        {
            houseService = _houseService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var allhouses = await houseService.LastThreeHousesAsync();

            return View(allhouses);
        }
    }
}
