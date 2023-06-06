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
                Id = model.Id,
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
            var user = await context.Users
                .Include(u => u.ApplicationUsersContacts)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid User ID!");
            }

            var contacts = await context.Contacts
                 .Include(u => u.ApplicationUsersContacts)
                 .ToListAsync();

            return contacts.Select(c => new ContactViewModel()
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
