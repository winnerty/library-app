using LibraryApp.Attributes;

namespace LibraryApp.Models
{
    public class Authors
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? YearOfBirth { get; set; }
        public string? Email { get; set; }

        public Authors() { }
    }
}
