using Contacts.Contracts;
using Contacts.Data;
using Contacts.Data.Entities;
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

        public async Task AddContactAsync(AddContactViewModel model)
        {
            var contact = new Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Website = model.WebSite
            };

            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
        }

        public async Task AddToTeamAsync(string userId, int contactId)
        {
            var user = await context.Users
                .Include(u => u.ApplicationUsersContacts)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Indalid User ID!");
            }
            var contact = await context.Contacts.FirstOrDefaultAsync(u => u.Id == contactId);
            if (contact == null)
            {
                throw new ArgumentException("Invalid Contact ID!");
            }
            user.ApplicationUsersContacts.Add(new ApplicationUserContact()
            {
                ApplicationUser = user,
                ApplicationUserId = userId,
                ContactId = contactId,
                Contact = contact
            });
            await context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Contact contact)
        {
            var model = await context.Contacts.FindAsync(contact.Id);

            if (model == null)
            {
                throw new ArgumentException("Invalid Contact ID!");
            }
            context.Contacts.Remove(model);

            await context.SaveChangesAsync();

        }

        public async Task EditContactAsync(ContactViewModel model, int contactId)
        {
            var contact = await context.Contacts.FindAsync(contactId);

            if (contact == null)
            {
                throw new ArgumentException("Contact with that ID do not exist !");
            }
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Address = model.Address;
            contact.Website = model.WebSite;
            contact.PhoneNumber = model.PhoneNumber;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContactsAsync()
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

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
            if (contact == null)
            {
                throw new ArgumentException("Invalid Contact ID!");
            }
            return contact;
        }

        public async Task<IEnumerable<ContactViewModel>> GetMyTeamAsync(string userId)
        {
            
            var contacts = await context.Contacts
                 .Include(c => c.ApplicationUsersContacts)
                 .ToListAsync();

            return contacts
                .Where(c=>c.ApplicationUsersContacts.Any(ac=>ac.ContactId==c.Id))
                .Select(c => new ContactViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address ?? "No Address",
                WebSite = c.Website

            }).ToList();
        }
    }
}
