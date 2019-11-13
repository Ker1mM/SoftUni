using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PANDA.Domain
{
    public class PandaUser : IdentityUser
    {
        //•	Has an Id – a GUID String or an Integer.
        //•	Has an Username
        //•	Has a Password
        //•	Has an Email
        //•	Has an Role – can be one of the following values(“User”, “Admin”)

        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }

    }
}
