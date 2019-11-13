using Eventures.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Domain
{
    public class EventuresUser : IdentityUser
    {
        //•	Username – a string (from IdentityUser).
        //•	Password – a string (from IdentityUser).
        //•	Email – a string (from IdentityUser).
        //•	First Name – a string.
        //•	Last Name – a string.
        //•	Unique Citizen Number(UCN) – a string.
        //•	Role – can be User / Admin


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "UCN should consist of 10 digits " , MinimumLength = 10)]
        public string UniqueCitizenNumber { get; set; }

        public UserRole Role { get; set; }
    }
}
