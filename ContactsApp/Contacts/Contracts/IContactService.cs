using Contacts.Data.Entities;
using Contacts.Models;


namespace Contacts.Contracts
{
    public interface IContactService
    {

        Task<IEnumerable<ContactViewModel>> GetAllContactsAsync();
        Task<IEnumerable<ContactViewModel>> GetMyTeamAsync(string userId);

        Task AddContactAsync(AddContactViewModel model);

        Task<AddContactViewModel> GetContactByIdAsync(int contactId);
        Task DeleteContactAsync(Contact contact);

        Task EditContactAsync(AddContactViewModel model, int contactId);

        Task AddToTeamAsync(string userId, int contactId);

    }
}
