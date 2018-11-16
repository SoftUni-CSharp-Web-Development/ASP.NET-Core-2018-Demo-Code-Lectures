using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IntroMvc.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntroMvc.ViewModels.Students
{
    public class FullName : IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "REQUIRED!!!")]
        public string LastName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.FirstName.Trim().EndsWith(".") && this.LastName.Trim().EndsWith("."))
            {
                yield return new ValidationResult("Please provide at least one full name.");
            }
        }
    }

    public class StudentBindingModel
    {
        public FullName Name { get; set; }

        [Display(Name = "Student type")]
        public StudentType Type { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        [StringLength(10, ErrorMessage = "{0} max len: {1}")]
        public string Bio { get; set; }

        [MinAge(18, ErrorMessage = "Min age: {0}")]
        public DateTime BirthDay { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal CoursesTaken { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }

    public enum StudentType
    {
        Onsite = 1,
        Online = 2,
        Fake = 3,
    }
}
