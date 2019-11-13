using Rider.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rider.Web.Areas.Cved.Models
{
    public class PartCreateModel
    {
        public PartCreateModel()
        {
            this.IsUsed = false;
        }

        public int? PartId { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [StringLength(20, ErrorMessage = "{0} length should be between {2} and {1} symbols.", MinimumLength = 3)]
        public string Make { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [StringLength(20, ErrorMessage = "{0} length should be between {2} and {1} symbols.", MinimumLength = 3)]
        public string Model { get; set; }


        [Required(ErrorMessage = "{0} field is required!")]
        [Range(0, 2000, ErrorMessage = "{0} of the part must be bigger than {1} and lower than {2}!")]
        public int? Weight { get; set; } //in grams


        [Required(ErrorMessage = "{0} field is required!")]
        [Range(0, 100, ErrorMessage = "{0} must be between {1} and {2}!")]
        [Display(Name ="Speed Rating")]
        public int? SpeedRating { get; set; }


        [Required(ErrorMessage = "{0} field is required!")]
        [Range(0, 100, ErrorMessage = "{0} must be between {1} and {2}!")]
        [Display(Name = "Suspension Rating")]
        public int? SuspensionRating { get; set; }


        [Required(ErrorMessage = "You have to select a type!")]
        [Range(1, int.MaxValue, ErrorMessage ="Please select a correct type!")]
        public PartType? Type { get; set; }


        [Required(ErrorMessage = "{0} field is required!")]
        [StringLength(250, ErrorMessage = "{0} length should be between {2} and {1} symbols.", MinimumLength = 3)]
        public string Description { get; set; }


        [Required(ErrorMessage = "{0} field is required!")]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "{0} must be between {1} and {2}!")]
        [Display(Name ="Base Price")]
        public decimal? BasePrice { get; set; }

        public bool IsUsed { get; set; }
    }
}
