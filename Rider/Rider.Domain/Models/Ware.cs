using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rider.Domain.Models
{
    public class Ware
    {
        public int Id { get; set; }

        public int PlayerPartId { get; set; }

        [ForeignKey(nameof(PlayerPartId))]
        public PlayerParts PlayerPart { get; set; }

        public DateTime ListedOn { get; set; }

        [Column(TypeName = "decimal(22, 2)")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}
