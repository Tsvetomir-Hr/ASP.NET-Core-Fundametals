using Contacts.Models;

namespace Contacts.Contracts
{
    public interface IContactService
    {

        Task<IEnumerable<ContactViewModel>> GetAllContacts();
    }
}
