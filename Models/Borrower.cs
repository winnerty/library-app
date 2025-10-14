using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Borrower
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

        public ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();
    }
}