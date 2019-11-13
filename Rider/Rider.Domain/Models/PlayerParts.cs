using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rider.Domain.Models
{
    public class PlayerParts
    {
        public int Id { get; set; }

        [Required]
        public string PlayerId { get; set; }

        public int PartId { get; set; }

        public Player Player { get; set; }

        public Part Part { get; set; }

        public bool IsUsed { get; set; }

        public bool IsForSale { get; set; }

        public int? BikePartId { get; set; }

        [ForeignKey(nameof(BikePartId))]
        public BikeParts BikePart { get; set; }
    }
}
