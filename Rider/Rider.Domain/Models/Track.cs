using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rider.Domain.Models
{
    public class Track
    {
        public Track()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Attempts = new HashSet<Attempt>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }


        public int Elevation { get; set; } //in meters, might be negative

        [Range(0, double.MaxValue)]
        public double Distance { get; set; } //in kilometers

        [Required]
        public TrackType Type { get; set; }

        public bool IsActive { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}
