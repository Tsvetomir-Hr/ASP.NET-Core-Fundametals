namespace HouseRentingSystem.Services.Interfaces
{
    using HouseRentingSystem.Web.ViewModels.Agent;

    public interface IAgentService
    {
        Task<bool> AgentExistsByUserIdAsync(string userid);

        Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> HasRentsByUserIdAsync(string userId);

        Task CreateAsync(string userId, BecomeAgentFormModel model);

        Task<string?> GetAgentIdByUserIdAsync(string userId);
    }
}
