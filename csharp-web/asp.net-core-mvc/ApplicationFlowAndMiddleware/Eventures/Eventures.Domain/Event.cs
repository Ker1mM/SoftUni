using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eventures.Domain
{
    public class Event
    {
        //•	Id – a UUID.
        //•	Name – a string.
        //•	Place – a string.
        //•	Start – a DateTime object.
        //•	End – a DateTime object.
        //•	Total Tickets – an integer.
        //•	Price Per Ticket – a decimal value.


        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalTickets { get; set; }

        public decimal PricePerTicket { get; set; }
    }
}
