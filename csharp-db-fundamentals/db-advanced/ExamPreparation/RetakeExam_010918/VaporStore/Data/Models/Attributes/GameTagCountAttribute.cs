using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.Data.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GameTagCountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gameTags = (HashSet<GameTag>)value;

            if (gameTags.Count == 0)
            {
                return new ValidationResult("Game Must Have At Least One Tag!");
            }

            return ValidationResult.Success;
        }
    }
}
