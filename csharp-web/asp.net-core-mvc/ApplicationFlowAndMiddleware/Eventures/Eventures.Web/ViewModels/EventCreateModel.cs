using Eventures.Web.Misc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Web.ViewModels
{
    public class EventCreateModel : IValidatableObject
    {

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(1000, ErrorMessage = "{0} should contain at least {2} characters.", MinimumLength = 10)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        public string Place { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DataType(DataType.DateTime, ErrorMessage = "{0} is not in correct format!")]
        public DateTime? Start { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DataType(DataType.DateTime, ErrorMessage = "{0} is not in correct format!")]
        public DateTime? End { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Display(Name = "Total Tickets")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} can not be below {1}!")]
        public int? TotalTickets { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Display(Name = "Price per Ticket")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} can not be below {1}!")]
        public decimal? PricePerTicket { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Start > End)
            {
                yield return new ValidationResult("End date should be after Start date!");
            }
        }
    }
}
