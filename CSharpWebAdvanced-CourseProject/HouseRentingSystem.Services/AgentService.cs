namespace HouseRentingSystem.Services
{
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext context;
        public AgentService(HouseRentingDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> AgentExistsByUserId(string userid)
        {
            bool isAlreadyAgent = await context.Agents
                 .AnyAsync(a => a.UserId.ToString() == userid);

            return isAlreadyAgent;
        }
    }
}
