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
                .Include(b => b.Author)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublicationYear = b.PublicationYear,
                    AuthorId = b.AuthorId,
                    AuthorName = b.Author.Name
                })
                .ToListAsync();
        }

        public async Task<BookDTO?> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                AuthorId = book.AuthorId,
                AuthorName = book.Author.Name
            };
        }

        public async Task<BookDTO> AddAsync(BookDTO bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                PublicationYear = bookDto.PublicationYear,
                AuthorId = bookDto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var createdBook = await _context.Books
                .Include(b => b.Author)
                .FirstAsync(b => b.Id == book.Id);

            return new BookDTO
            {
                Id = createdBook.Id,
                Title = createdBook.Title,
                PublicationYear = createdBook.PublicationYear,
                AuthorId = createdBook.AuthorId,
                AuthorName = createdBook.Author.Name
            };
        }

        public async Task<BookDTO?> UpdateAsync(int id, BookDTO bookDto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            book.Title = bookDto.Title;
            book.PublicationYear = bookDto.PublicationYear;
            book.AuthorId = bookDto.AuthorId;

            await _context.SaveChangesAsync();

            var updatedBook = await _context.Books
                .Include(b => b.Author)
                .FirstAsync(b => b.Id == book.Id);

            return new BookDTO
            {
                Id = updatedBook.Id,
                Title = updatedBook.Title,
                PublicationYear = updatedBook.PublicationYear,
                AuthorId = updatedBook.AuthorId,
                AuthorName = updatedBook.Author.Name
            };
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