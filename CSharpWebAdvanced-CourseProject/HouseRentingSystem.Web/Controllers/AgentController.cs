using HouseRentingSystem.Services.Interfaces;
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

            bool isAgent = await agentService.AgentExistsByUserId(userId!);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already agent!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
