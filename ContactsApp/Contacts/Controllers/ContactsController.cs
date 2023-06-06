using Contacts.Contracts;
using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService contactService;
        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await contactService.GetAllContactsAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> MyTeam()
        {
            try
            {
                var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

                var myTeam = await contactService.GetMyTeamAsync(userId);

                return View(myTeam);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddContactViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await contactService.AddContactAsync(model);

            return RedirectToAction("All", "Contacts");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int contactId)
        {

            try
            {

                var contact = await contactService.GetContactByIdAsync(contactId);

                var model = new AddContactViewModel()
                {
                    Id = contactId,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    Address = contact.Address ?? "No Address",
                    WebSite = contact.Website
                };

                await contactService.DeleteContactAsync(contact);


                return View(model);


            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddContactViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await contactService.AddContactAsync(model);

            return RedirectToAction("All", "Contacts");

        }
    }
}
