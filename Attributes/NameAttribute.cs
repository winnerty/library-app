using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Attributes
{
    public sealed class NameAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            if (value is null) return ValidationResult.Success;
            if (value is not string s)
                return new ValidationResult("Name must be a string");
           
            if (s.Length > 20)
                return new ValidationResult("max len is 20");
            else
                return ValidationResult.Success;
        }

    }
}