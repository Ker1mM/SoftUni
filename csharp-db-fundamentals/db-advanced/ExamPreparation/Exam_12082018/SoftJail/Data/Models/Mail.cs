using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoftJail.Data.Models
{
    [Table("Mails")]
    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z0-9 ]+ str.)$")]
        public string Address { get; set; }

        public int PrisonerId { get; set; }

        [ForeignKey(nameof(PrisonerId))]
        public Prisoner Prisoner { get; set; }
    }
}
