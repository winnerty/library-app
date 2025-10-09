using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Attributes
{
    public class YearOfBirthAttribute : ValidationAttribute
    {
        public int Min { get; set; } = 1300;
        public int? Max { get; set; } = 2025;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            int year;
            
            if (value is int i)
                year = i;
            else if (value is string s && int.TryParse(s, out var parsed))
                year = parsed;
            else
                return new ValidationResult("YearOfBirth must be an integer year");


            if (year < Min || year > Max)
                return new ValidationResult("YearOfBirth should be between 1300 and 2025");

            return ValidationResult.Success;
        }
    }
}
