
namespace HouseRentingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using HouseRentingSystem.Services.Interfaces;
    using HouseRentingSystem.Web.ViewModels.House;

    using static Common.NotificationMessageConstants;
    using HouseRentingSystem.Services.Data.Models.House;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;


        public HouseController(
            IHouseService houseService,
            ICategoryService categoryService,
            IAgentService agentService)
        {
            this.houseService = houseService;
            this.categoryService = categoryService;
            this.agentService = agentService;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel queryModel)
        {
            AllHousesFilteredAndPagedServiceModel serviceModel = await this.houseService.AllAsync(queryModel);

            queryModel.Houses = serviceModel.Houses;
            queryModel.TotalHouses = serviceModel.TotalHousesCount;
            queryModel.Categories = await categoryService.AllCategoryNamesAsync();


            return View(queryModel);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await agentService.AgentExistsByUserIdAsync(GetUserId()!);
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return RedirectToAction("Become", "Agent");
            }
            try
            {
                HouseFormModel model = new HouseFormModel()
                {
                    Categories = await categoryService.AllCategoriesAsync()
                };

                return View(model);
            }
            catch (Exception)
            {
                return GeneralEror();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            bool isAgent = await agentService.AgentExistsByUserIdAsync(GetUserId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return RedirectToAction("Become", "Agent");
            }
            bool categoryExists = await categoryService.ExistByIdAsync(model.CategoryId);

            if (!categoryExists)
            {
                //When we add error to the ModelState it becomes Invalid.
                ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }

            try
            {
                string? agentId = await agentService.GetAgentIdByUserIdAsync(GetUserId()!);

                await this.houseService.CreateAsync(model, agentId!);

                return RedirectToAction("All", "House");
            }
            catch (Exception _)
            {

                ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add your new house! Please try again leter or contact the administartor!");

                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }


        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            bool houseExist = await houseService.ExistByIdAsync(id);

            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            }


            try
            {
                HouseDetailsViewModel model = await houseService
                .GetDetailsByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {


                return GeneralEror();

            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<HouseAllViewModel> myHouses = new List<HouseAllViewModel>();

            string? userId = GetUserId();
            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(userId!);
            try
            {
                if (isUserAgent)
                {
                    string? agentId = await this.agentService.GetAgentIdByUserIdAsync(userId!);

                    myHouses.AddRange(await houseService.AllByAgentIdAsync(agentId!));

                }
                myHouses.AddRange(await this.houseService.AllByUserIdAsync(userId!));

                return View(myHouses);
            }
            catch (Exception)
            {
                return GeneralEror();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool houseExist = await houseService.ExistByIdAsync(id);

            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            }

            bool isUserAgent = await agentService
                .AgentExistsByUserIdAsync(GetUserId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await agentService.GetAgentIdByUserIdAsync(GetUserId()!);

            bool isAgentOwner = await houseService
                .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

                return RedirectToAction("Mine", "House");
            }

            try
            {
                HouseFormModel formModel = await houseService
                .GetHouseForEditAsync(id);

                formModel.Categories = await categoryService.AllCategoriesAsync();

                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralEror();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, HouseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }
            bool houseExist = await houseService.ExistByIdAsync(id);

            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            }

            bool isUserAgent = await agentService
                .AgentExistsByUserIdAsync(GetUserId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await agentService.GetAgentIdByUserIdAsync(GetUserId()!);

            bool isAgentOwner = await houseService
                .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

                return RedirectToAction("Mine", "House");
            }

            try
            {
                await houseService.EditHouseByIdAndFormModel(id, model);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to edit the house. Please try again later or contact administrator. ");

                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("Details", "House", new { id });

        }


        private IActionResult GeneralEror()
        {
            this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later or contact administrator.";

            return RedirectToAction("Index", "Home");
        }



    }
}
