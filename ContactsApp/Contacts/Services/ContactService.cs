using Contacts.Contracts;
using Contacts.Data;
using Contacts.Models;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Contacts.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactsDbContext context;
        public ContactService(ContactsDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ContactViewModel>> GetAllContacts()
        {
            var models = await context.Contacts.ToListAsync();

            return models.Select(x => new ContactViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x?.Address ?? "No address",
                WebSite = x!.Website
            });
        }
    }
}
