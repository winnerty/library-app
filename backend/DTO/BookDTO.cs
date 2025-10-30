using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Range(1600, 2025)]
        public int PublicationYear { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public string AuthorName { get; set; } = string.Empty;
    }
}