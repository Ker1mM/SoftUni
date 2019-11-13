using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PANDA.Domain
{
    public class Receipt
    {
        //•	Has an Id – a GUID String or an Integer.
        //•	Has a Fee – a decimal number.
        //•	Has an Issued On – a DateTime object.
        //•	Has a Recipient – a User object.
        //•	Has a Package – a Package object.

        public int Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        [Required]
        public string RecipientId { get; set; }

        [ForeignKey(nameof(RecipientId))]
        public virtual PandaUser Recipient { get; set; }

        public int PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual Package Package { get; set; }
    }
}
