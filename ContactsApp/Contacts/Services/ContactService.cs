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

        public async Task AddContactAsync(FormContactViewModel model)
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

        public async Task AddToTeamAsync(string userId, FormContactViewModel model)
        {
            var user = await context.Users
                .Include(u => u.ApplicationUsersContacts)
                .FirstAsync(u => u.Id == userId);

            var contact = await context.Contacts.FirstOrDefaultAsync(u => u.Id == model.Id);

            bool isAlreadyAdded = user.ApplicationUsersContacts.Any(uc=>uc.ApplicationUserId==userId && uc.ContactId==model.Id);

            if (!isAlreadyAdded)
            {
                user.ApplicationUsersContacts.Add(new ApplicationUserContact()
                {
                    ApplicationUserId = userId,
                    ContactId = model.Id
                });

                await context.SaveChangesAsync();
            }
           
        }


        public async Task EditContactAsync(FormContactViewModel model, int contactId)
        {

            var contact = await context.Contacts.FindAsync(contactId);

            if (contact!=null)
            {
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Address = model.Address;
            contact.Website = model.WebSite;
            contact.PhoneNumber = model.PhoneNumber;

            }

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContactsAsync()
        {
            var models = await context.Contacts.ToListAsync();


            return await context
                .Contacts
                .Select(x => new ContactViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address!,
                    WebSite = x!.Website
                }).ToListAsync();
        }


        public async Task<FormContactViewModel?> GetContactByIdAsync(int contactId)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);

            return await context.Contacts
                .Where(c => c.Id == contactId)
                .Select(c => new FormContactViewModel()
                {
                    Id = contactId,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    Address = contact.Address ?? "No address",
                    WebSite = contact.Website
                })
                .FirstOrDefaultAsync();
           
        }

        public async Task<IEnumerable<ContactViewModel>> GetMyTeamAsync(string userId)
        {

            var contacts = await context.Contacts
                 .Include(c => c.ApplicationUsersContacts)
                 .ToListAsync();

            return contacts
                .Where(c => c.ApplicationUsersContacts.Any(ac => ac.ContactId == c.Id))
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

        public async Task RemoveFromTeamAsync(string userId, FormContactViewModel contact)
        {
            var user = await context.Users
               .Include(u => u.ApplicationUsersContacts)
               .FirstAsync(u => u.Id == userId);

            var userContactToRemove = user.ApplicationUsersContacts.FirstOrDefault(ac => ac.ApplicationUserId == userId && ac.ContactId == contact.Id);
            if (userContactToRemove != null)
            {
                user.ApplicationUsersContacts.Remove(userContactToRemove);

                await context.SaveChangesAsync();
            }
        }
    }
}
