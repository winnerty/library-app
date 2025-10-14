using LibraryApp.Data;
using LibraryApp.DTO;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services
{
    public class BookService
    {
        private readonly LibraryAppContext _context;

        public BookService(LibraryAppContext context)
        {
            _context = context;
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    PublicationYear = b.PublicationYear,
                    AuthorId = b.AuthorId,
                    AuthorName = b.Author.Name
                })
                .ToListAsync();
        }

        public async Task<BookDTO?> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                AuthorId = book.AuthorId,
                AuthorName = book.Author.Name
            };
        }

        public async Task<BookDTO> AddAsync(BookDTO dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                PublicationYear = dto.PublicationYear,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            dto.Id = book.Id;
            return dto;
        }

        public async Task<BookDTO?> UpdateAsync(int id, BookDTO dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            book.Title = dto.Title;
            book.ISBN = dto.ISBN;
            book.PublicationYear = dto.PublicationYear;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}