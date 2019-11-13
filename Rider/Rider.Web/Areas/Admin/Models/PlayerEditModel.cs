using Rider.Domain.Models;
using Rider.Web.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Areas.Admin.Models
{
    public class PlayerEditModel : IValidatableObject
    {
        [Required]
        public string Username { get; set; }

        public string Role { get; set; }

        public string NewRole { get; set; }

        public IEnumerable<string> AllRoles  => Enum.GetValues(typeof(Role)).Cast<Role>().Select(x => x.ToString()).ToList();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Role != null && Role == NewRole)
            {
                yield return new ValidationResult("New role can't be the same as the old role!");
            }
        }
    }
}
