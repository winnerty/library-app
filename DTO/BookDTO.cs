using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(13)]
        public string? ISBN { get; set; }

        public int? PublicationYear { get; set; }

        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}