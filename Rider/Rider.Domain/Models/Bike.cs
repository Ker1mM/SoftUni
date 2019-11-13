using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rider.Domain.Models
{
    public class Bike
    {
        public Bike()
        {
            this.BikeParts = new HashSet<BikeParts>();
            this.Attempts = new HashSet<Attempt>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Incorrect bike nickname length!")]
        public string Nickname { get; set; }

        public BikeType Type { get; set; }

        [Required]
        public string PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        public virtual ICollection<BikeParts> BikeParts { get; set; }

        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}
