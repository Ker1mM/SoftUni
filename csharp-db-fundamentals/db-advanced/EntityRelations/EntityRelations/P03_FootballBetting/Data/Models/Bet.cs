using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    [Table("Bets")]
    public class Bet
    {
        [Key]
        public int BetId { get; set; }

        [Column(TypeName = "MONEY")]
        public decimal Amount { get; set; }

        [Required]
        public string Prediction { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
