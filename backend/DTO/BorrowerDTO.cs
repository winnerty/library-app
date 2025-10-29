using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTO
{
    public class BorrowerDTO
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}