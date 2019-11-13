using Eventures.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventures.Services.EventServices
{
    public interface IEventService
    {
        List<Event> GetAllEvents();

        Event CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket);
    }
}
