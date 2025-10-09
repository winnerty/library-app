using LibraryApp.Attributes;

namespace LibraryApp.DTO
{

    public class AuthorsDTO
    {
        public int Id { get; set; }

        [Name]
        public string? Name { get; set; }
        [YearOfBirth]

        public int? YearOfBirth { get; set; }
        [Email]
        public string? Email { get; set; }

        public AuthorsDTO() { }
    }
}
