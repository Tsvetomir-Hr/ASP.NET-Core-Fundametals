using HouseRentingSystem.Services.Interfaces;
using HouseRentingSystem.Web.ViewModels.Agent;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Controllers
{
    using static HouseRentingSystem.Common.NotificationMessageConstants;
    public class AgentController : BaseController
    {
        private readonly IAgentService agentService;
        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = GetUserId();

            bool isAgent = await agentService.AgentExistsByUserIdAsync(userId!);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already agent!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {

            string? userId = GetUserId();

            bool isAgent = await agentService.AgentExistsByUserIdAsync(userId!);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already agent!";

                return RedirectToAction("Index", "Home");
            }
            bool isPhoneNumberAlreadyTaken = await agentService.AgentExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberAlreadyTaken)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with provided phone number already exists!");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool UserHasActiveRents = await agentService.HasRentsByUserIdAsync(userId!);

            if (UserHasActiveRents)
            {
                this.TempData[ErrorMessage] = "You must not have any active rents in order to become an agent!";

                return RedirectToAction("Mine", "House");
            }

            try
            {
                await agentService.CreateAsync(userId!, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured while registering you as an agent! Please try again leter or contact administrator.";

                return RedirectToAction("Index","Home");
                
            }
            return RedirectToAction("All", "House");
        }
    }
}
