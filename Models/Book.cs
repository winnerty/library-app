using LibraryApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(13)]
        public string? ISBN { get; set; }

        public int? PublicationYear { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
    }
}