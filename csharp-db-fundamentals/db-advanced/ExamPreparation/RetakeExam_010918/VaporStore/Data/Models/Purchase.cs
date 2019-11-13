using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VaporStore.Data.Enums;

namespace VaporStore.Data.Models
{
    [Table("Purchases")]
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PurchaseType Type { get; set; }

        [Required]
        [RegularExpression("^([0-9A-Z]{4})(-)([0-9A-Z]{4})(-)([0-9A-Z]{4})$")]
        public string ProductKey { get; set; }

        public DateTime Date { get; set; }

        public int CardId { get; set; }

        public int GameId { get; set; }

        [ForeignKey(nameof(CardId))]
        public Card Card { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}
