namespace HouseRentingSystem.Services
{
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Data.Models;
    using HouseRentingSystem.Services.Interfaces;
    using HouseRentingSystem.Web.ViewModels.Agent;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext context;
        public AgentService(HouseRentingDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool isAlreadyAgent = await context.Agents
                  .AnyAsync(a => a.PhoneNumer == phoneNumber);

            return isAlreadyAgent;
        }

        public async Task<bool> AgentExistsByUserIdAsync(string userid)
        {
            bool isAlreadyAgent = await context.Agents
                 .AnyAsync(a => a.UserId.ToString() == userid);

            return isAlreadyAgent;
        }

        public async Task CreateAsync(string userId, BecomeAgentFormModel model)
        {
            Agent agent = new Agent()
            {
                PhoneNumer = model.PhoneNumber,
                UserId = Guid.Parse(userId),
            };

            await this.context.Agents.AddAsync(agent);
            await this.context.SaveChangesAsync();

        }

        public async Task<bool> HasRentsByUserIdAsync(string userId)
        {
            ApplicationUser? user = await context.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            return user.RentedHouses.Any();
        }

    }
}
