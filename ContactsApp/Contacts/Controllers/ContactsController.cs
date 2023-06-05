using Contacts.Contracts;
using Microsoft.AspNetCore.Mvc;

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
            var models = await contactService.GetAllContacts();

            return View(models);
        }
    }
}
