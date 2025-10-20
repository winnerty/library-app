using LibraryApp.Data;
using LibraryApp.DTO;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services
{
    public class AuthorService
    {
        private readonly LibraryAppContext _context;

        public AuthorService(LibraryAppContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorDTO>> GetAllAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    YearOfBirth = a.YearOfBirth,
                    Email = a.Email
                })
                .ToListAsync();
        }

        public async Task<AuthorDTO?> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return null;

            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                YearOfBirth = author.YearOfBirth,
                Email = author.Email
            };
        }

        public async Task<AuthorDTO> AddAsync(AuthorDTO authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                YearOfBirth = authorDto.YearOfBirth,
                Email = authorDto.Email
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                YearOfBirth = author.YearOfBirth,
                Email = author.Email
            };
        }

        public async Task<bool> UpdateAsync(int id, AuthorDTO authorDto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            author.Name = authorDto.Name;
            author.YearOfBirth = authorDto.YearOfBirth;
            author.Email = authorDto.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}