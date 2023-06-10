using Contacts.Data.Entities;
using Contacts.Models;


namespace Contacts.Contracts
{
    public interface IContactService
    {

        Task<IEnumerable<ContactViewModel>> GetAllContactsAsync();

        Task<IEnumerable<ContactViewModel>> GetMyTeamAsync(string userId);

        Task AddContactAsync(FormContactViewModel model);

        Task<FormContactViewModel?> GetContactByIdAsync(int contactId);

        Task EditContactAsync(FormContactViewModel model, int contactId);

        Task AddToTeamAsync(string userId, FormContactViewModel model);

        Task RemoveFromTeamAsync(string userId, FormContactViewModel contact);
    }
}
