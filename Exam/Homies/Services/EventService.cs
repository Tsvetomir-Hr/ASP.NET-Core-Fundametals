using Homies.Contracts;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext context;
        public EventService(HomiesDbContext context)
        {
            this.context = context;
        }

        public async Task AddEventAsync(FormEventViewModel model, string userId)
        {

            var eventToAdd = new Event()
            {

                Name = model.Name,
                Description = model.Description,
                Start = DateTime.Parse(model.Start),
                End = DateTime.Parse(model.End),
                TypeId = model.TypeId,
                OrganiserId = userId

            };
            await context.Events.AddAsync(eventToAdd);
            await context.SaveChangesAsync();
        }

        public async Task EditEventAsync(int eventId, FormEventViewModel model)
        {
            var eventToEdit = await context.Events.FindAsync(eventId);  
            
            if (eventToEdit != null)
            {
                eventToEdit.Name = model.Name;
                eventToEdit.Description = model.Description;
                eventToEdit.Start = DateTime.Parse(model.Start);
                eventToEdit.End = DateTime.Parse(model.End);
                eventToEdit.TypeId = model.TypeId;

            }
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync()
        {
            return await context.Events.Select(e => new AllEventsViewModel()
            {
                Name = e.Name,
                Start = e.Start.ToString("MM-dd-yy HH:mm"),
                Type = e.Type.Name,
                Id = e.Id,
                Organiser = e.Organiser.Email
            }).ToListAsync();
        }

        public async Task<FormEventViewModel> GetEventByIdAsync(int eventId)
        {
            return await context.Events
                .Where(e => e.Id == eventId)
                .Select(e => new FormEventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString("dd/MM/yyyy H:mm"),
                    End = e.End.ToString("dd/MM/yyyy H:mm"),
                    TypeId = e.TypeId 
                }).FirstAsync();
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetJoinedEventsAsync(string userId)
        {
            return await context.Events
                .Where(e => e.EventsParticipants.Any(ep => ep.HelperId == userId))
                .Select(e => new AllEventsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString("MM-dd-yy HH:mm"),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.Email
                }).ToListAsync();

        }

        public async Task<IEnumerable<TypeViewModel>> GetTypesAsync()
        {
            return await context.Types.Select(t => new TypeViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();
        }

        public async Task JoinEventAsync(string userId, int eventId)
        {
            bool isAlreadyJoined = await context.EventsParticipants.AnyAsync(ep => ep.HelperId == userId && ep.EventId == eventId);
            if (!isAlreadyJoined)
            {
                await context.EventsParticipants.AddAsync(new EventParticipant()
                {
                    EventId = eventId,
                    HelperId = userId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task LeaveEventAsync(string userId, int eventId)
        {
            var eventToLeave = await context.EventsParticipants.FirstAsync(ep => ep.EventId == eventId && ep.HelperId == userId);

            if (eventToLeave != null)
            {
                context.EventsParticipants.Remove(eventToLeave);

                await context.SaveChangesAsync();
            }
        }
    }
}
