namespace HouseRentingSystem.Services.Interfaces
{
    public interface IAgentService
    {
        Task<bool> AgentExistsByUserId(string userid);
    }
}
