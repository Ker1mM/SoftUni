using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rider.Domain.Models
{
    public class BikeParts
    {
        public int Id { get; set; }

        public int BikeId { get; set; }

        public Bike Bike { get; set; }

        public int PlayerPartId { get; set; }

        [ForeignKey(nameof(PlayerPartId))]
        public PlayerParts PlayerPart { get; set; }
    }
}
