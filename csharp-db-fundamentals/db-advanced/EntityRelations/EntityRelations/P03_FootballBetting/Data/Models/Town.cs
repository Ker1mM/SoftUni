using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    [Table("Towns")]
    public class Town
    {
        public Town()
        {
            Teams = new HashSet<Team>();
        }

        [Key]
        public int TownId { get; set; }

        [Required]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public ICollection<Team> Teams { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
    }
}
