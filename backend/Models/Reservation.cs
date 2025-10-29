namespace LibraryApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public bool IsActive { get; set; } = true;
    }
}