using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rider.Domain.Models
{
    public class Player : IdentityUser
    {
        public Player()
        {
            this.Attempts = new HashSet<Attempt>();
            this.Bikes = new HashSet<Bike>();
            this.Inventory = new HashSet<PlayerParts>();
        }

        [Column(TypeName = "decimal(22, 2)")]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual  ICollection<Attempt> Attempts { get; set; }

        public virtual ICollection<Bike> Bikes { get; set; }

        public ICollection<PlayerParts> Inventory { get; set; }
    }
}
