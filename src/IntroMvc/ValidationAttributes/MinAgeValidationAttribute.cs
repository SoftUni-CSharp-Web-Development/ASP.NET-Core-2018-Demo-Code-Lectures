using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroMvc.ValidationAttributes
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int age;

        public MinAgeAttribute(int age)
        {
            this.age = age;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is DateTime))
            {
                return new ValidationResult("Invalid DateTime object.");
            }

            var dateTime = (DateTime)value;
            if (((DateTime.UtcNow - dateTime).TotalDays / 365.25) < this.age)
            {
                return new ValidationResult(this.ErrorMessage.Replace("{0}", this.age.ToString()));
            }

            return ValidationResult.Success;
        }
    }
}
