using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VaporStore.Data.Enums;

namespace VaporStore.Data.Models
{
    [Table("Cards")]
    public class Card
    {
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("^([0-9]{4})( +)([0-9]{4})( +)([0-9]{4})( +)([0-9]{4})$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression("^([0-9]{3})$")]
        public string Cvc { get; set; }

        [Required]
        public CardType Type { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

    }
}
