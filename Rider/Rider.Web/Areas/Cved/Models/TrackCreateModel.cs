using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Areas.Cved.Models
{
    public class TrackCreateModel
    {
        public string TrackId { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [StringLength(25, ErrorMessage = "{0} length should be between {2} and {1} symbols.", MinimumLength = 3)]
        public string Name { get; set; }


        [Required(ErrorMessage = "{0} field is required!")]
        [StringLength(500, ErrorMessage = "{0} length should be between {2} and {1} symbols.", MinimumLength = 10)]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [Range(0, 5000, ErrorMessage = "Please enter realistic value.")]
        public int? Elevation { get; set; } //in meters

        [Required(ErrorMessage = "{0} field is required!")]
        [Range(10, 500, ErrorMessage = "Track distance must be above {1} and below {2}")]
        public double? Distance { get; set; } //in kilometers


        [Required(ErrorMessage = "You have to select a type!")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a correct type!")]
        public TrackType? Type { get; set; }
    }
}
