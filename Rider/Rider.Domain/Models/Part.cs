using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rider.Domain.Models
{
    public class Part
    {
        public Part()
        {
            this.PlayerParts = new HashSet<PlayerParts>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Make { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Model { get; set; }

        [Range(0, int.MaxValue)]
        public int Weight { get; set; } //in grams

        [Range(0, 100)]
        public int SpeedRating { get; set; }

        [Range(0, 100)]
        public int SuspensionRating { get; set; }

        [Required]
        public PartType Type { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(22, 2)")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal BasePrice { get; set; }

        public ICollection<PlayerParts> PlayerParts { get; set; }
    }
}
