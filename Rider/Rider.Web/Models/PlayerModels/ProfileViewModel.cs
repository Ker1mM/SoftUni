using System;

namespace Rider.Web.Models.PlayerModels
{
    public class ProfileViewModel
    {
        public string Username { get; set; }

        public decimal Balance { get; set; }

        public string Role { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Email { get; set; }
    }
}
