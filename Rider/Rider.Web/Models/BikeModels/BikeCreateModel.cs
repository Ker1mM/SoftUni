using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Models.BikeModels
{
    public class BikeCreateModel
    {
        [Required(ErrorMessage = "{0} field is required!")]
        [StringLength(20, ErrorMessage = "{0} length should be between {2} and {1} symbols.", MinimumLength = 3)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "You have to select a type!")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a correct type!")]
        public BikeType? Type { get; set; }
    }
}
