using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Range(1900, 2025)]
        public int YearOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}