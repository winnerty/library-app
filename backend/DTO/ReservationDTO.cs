using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }

        [Required]
        public int BorrowerId { get; set; }

        public string BorrowerName { get; set; } = string.Empty;

        [Required]
        public int BookId { get; set; }
        
        public string BookTitle { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}