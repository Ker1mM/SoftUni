using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Web.Misc
{
    public class IsNotBefore : ValidationAttribute
    {
        private const string DateTimeFormat = "dd/MM/yyyy";
        private readonly DateTime date;

        public IsNotBefore(string dateInput)
        {
            this.date = DateTime.Parse(dateInput, CultureInfo.InvariantCulture);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value < date)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
