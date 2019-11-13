using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.Domain;
using Eventures.Services.EventServices;
using Eventures.Web.Misc;
using Eventures.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [Authorize]
        public IActionResult All()
        {
            var model = this.eventService.GetAllEvents()
                .Select(x => new EventViewModel
                {
                    Name = x.Name,
                    Start = x.Start,
                    End = x.End,
                    Place = x.Place

                }).ToList();

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(EventCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                var result = new StringBuilder();

                foreach (var error in ModelState.Values.SelectMany(x => x.Errors))
                {
                    result.AppendLine(error.ErrorMessage.ToString());
                }

                throw new InvalidModelStateException(result.ToString().TrimEnd());
            }

            this.eventService.CreateEvent(model.Name, model.Place,
                (DateTime)model.Start,
                (DateTime)model.End,
                (int)model.TotalTickets,
                (decimal)model.PricePerTicket);

            return this.RedirectToAction(nameof(All));
        }

    }

}