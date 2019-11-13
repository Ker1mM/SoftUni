using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eventures.Data;
using Eventures.Domain;

namespace Eventures.Services.EventServices
{
    public class EventService : IEventService
    {
        private readonly EventuresDbContext context;

        public EventService(EventuresDbContext context)
        {
            this.context = context;
        }

        public Event CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket)
        {
            var newEvent = new Event()
            {
                Name = name,
                Place = place,
                Start = start,
                End = end,
                TotalTickets = totalTickets,
                PricePerTicket = pricePerTicket
            };

            this.context.Events.Add(newEvent);
            this.context.SaveChanges();

            return newEvent;
        }

        public List<Event> GetAllEvents()
        {
            var events = this.context.Events.ToList();

            events = events.OrderBy(x => x.Start).ToList();
            return events;
        }
    }
}
