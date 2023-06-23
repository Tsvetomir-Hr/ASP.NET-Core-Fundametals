using Homies.Models;

namespace Homies.Contracts
{
    public interface IEventService
    {
        public Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync();

        public Task<IEnumerable<AllEventsViewModel>> GetJoinedEventsAsync(string userId);

        public Task<IEnumerable<TypeViewModel>> GetTypesAsync();

        public Task AddEventAsync(FormEventViewModel model, string userId);

        public Task JoinEventAsync(string userId, int eventId);

        public Task LeaveEventAsync(string userId, int eventId);

        public Task<FormEventViewModel> GetEventByIdAsync(int eventId);

        public Task EditEventAsync(int eventId,FormEventViewModel model);

    }
}
