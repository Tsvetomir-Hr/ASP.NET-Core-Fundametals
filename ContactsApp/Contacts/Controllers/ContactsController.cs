using Contacts.Contracts;
using Contacts.Data.Entities;
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
        public async Task<IActionResult> Team()
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
            var model = new FormContactViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(FormContactViewModel model)
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
            var model = await contactService.GetContactByIdAsync(contactId);

            return View(model);



        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FormContactViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);

            }

            await contactService.EditContactAsync(model, id);

            return RedirectToAction("All", "Contacts");

        }

        [HttpPost]
        public async Task<IActionResult> AddToTeam(int contactId)
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

            var contact = await contactService.GetContactByIdAsync(contactId);

            await contactService.AddToTeamAsync(userId, contact);


            return RedirectToAction("All", "Contacts");

        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromTeam(int contactId)
        {
            var contact = await contactService.GetContactByIdAsync(contactId);

            if (contact == null)
            {
                return RedirectToAction(nameof(Team));
            }

            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

            await contactService.RemoveFromTeamAsync(userId,contact);

            return RedirectToAction(nameof(Team));
        }
    }
}
