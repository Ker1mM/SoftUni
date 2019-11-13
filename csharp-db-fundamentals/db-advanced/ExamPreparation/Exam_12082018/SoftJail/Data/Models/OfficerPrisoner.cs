using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoftJail.Data.Models
{
    [Table("OfficersPrisoners")]
    public class OfficerPrisoner
    {
        public int PrisonerId { get; set; }

        public int OfficerId { get; set; }

        [ForeignKey(nameof(OfficerId))]
        public Officer Officer { get; set; }

        [ForeignKey(nameof(PrisonerId))]
        public Prisoner Prisoner { get; set; }
    }
}
