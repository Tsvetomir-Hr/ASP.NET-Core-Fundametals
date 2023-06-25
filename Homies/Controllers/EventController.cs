using Homies.Contracts;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;
        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {

            var models = await eventService.GetAllEventsAsync();

            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var userId = GetUserId();

            var models = await eventService.GetJoinedEventsAsync(userId);

            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new FormEventViewModel()
            {
                Types = await eventService.GetTypesAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(FormEventViewModel model)
        {
            var orginizerId = GetUserId();

            if (!ModelState.IsValid)
            {
                model.Types = await eventService.GetTypesAsync();

                ModelState.AddModelError("", "Invalid data!");

                return View(model);
            }

            try
            {
                await eventService.AddEventAsync(model, orginizerId);


            }
            catch (InvalidDataException ex)
            {
                model.Types = await eventService.GetTypesAsync();

                ModelState.AddModelError("", ex.Message);
                return View(model);


            }

            return RedirectToAction(nameof(All));


        }
        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {

            var userId = GetUserId();

            await eventService.JoinEventAsync(userId, id);

            return RedirectToAction(nameof(Joined));
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var userId = GetUserId();

            await eventService.LeaveEventAsync(userId, id);

            return RedirectToAction(nameof(All));

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await eventService.GetEventByIdAsync(id);

            model.Types = await eventService.GetTypesAsync();

            return View(model);
        }
        public async Task<IActionResult> Edit(int id, FormEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Types = await eventService.GetTypesAsync();

                return View(model);
            }
            try
            {
                await eventService.EditEventAsync(id, model);
            }
            catch (InvalidDataException ex)
            {

                model.Types = await eventService.GetTypesAsync();

                ModelState.AddModelError("", ex.Message);

                return View(model);

            }



            return RedirectToAction(nameof(All));



        }
    }
}
