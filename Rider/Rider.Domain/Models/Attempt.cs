using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rider.Domain.Models
{
    public class Attempt
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime AttemptedOn { get; set; }

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }

        public int PlayerBikeId { get; set; }

        public Bike PlayerBike { get; set; }

        [Required]
        public string TrackId { get; set; }

        public Track Track { get; set; }
    }
}
