using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTO
{
    public class BorrowerDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}