using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LibraryApp.Attributes
{
    public class EmailAttribute : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new(
            @"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            if (value is null)
                return ValidationResult.Success;

            if (value is not string s)
                return new ValidationResult(ErrorMessage ?? "Email should match the format");

            s = s.Trim();
            if (s.Length == 0) 
                return ValidationResult.Success;

            if (!EmailRegex.IsMatch(s))
                return new ValidationResult(ErrorMessage ?? "Email should match the format like name@domain.tld");

            var at = s.IndexOf('@');
            var domain = s[(at + 1)..];
            if (domain.Contains(".."))
                return new ValidationResult(ErrorMessage ?? "Domain cannot contain consecutive dots");
            
            foreach (var label in domain.Split('.'))
            {
                if (label.Length == 0 || label.StartsWith('-') || label.EndsWith('-'))
                    return new ValidationResult(ErrorMessage ?? "Invalid domain label");
            }

            return ValidationResult.Success;
        }
    }
}