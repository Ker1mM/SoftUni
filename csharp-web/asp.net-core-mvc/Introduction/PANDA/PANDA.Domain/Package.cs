using PANDA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PANDA.Domain
{
    public class Package
    {
        //•	Has an Id – a GUID String or an Integer.
        //•	Has a Description – a string.
        //•	Has a Weight – a floating-point number.
        //•	Has a Shipping Address – a string.
        //•	Has a Status – can be one of the following values (“Pending”, “Shipped”, “Delivered”, “Acquired”)
        //•	Has an Estimated Delivery Date – A DateTime object.
        //•	Has a Recipient – a User object.


        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Weight { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        public PackageSatus Status { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual PandaUser Recipient { get; set; }

        [Required]
        public string ReceiptId { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}
