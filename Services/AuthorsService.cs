using LibraryApp.Data;
using LibraryApp.DTO;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;

namespace LibraryApp.Services
{
    public class AuthorsService
    {
        private readonly LibraryAppContext _context;

        public AuthorsService(LibraryAppContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorsDTO>> GetAllAsync()
        {
            return await _context.Authors
                .AsNoTracking()
                .Select(a => new AuthorsDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    YearOfBirth = a.YearOfBirth,
                    Email = a.Email
                })
                .ToListAsync();
        }

        public async Task<AuthorsDTO?> GetByIdAsync(int id)
        {
            var a = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (a is null) return null;
            return new AuthorsDTO
            {
                Id = a.Id,
                Name = a.Name,
                YearOfBirth = a.YearOfBirth,
                Email = a.Email
            };
        }

        public async Task<AuthorsDTO> AddAsync(AuthorsDTO dto)
        {
            var entity = new Authors
            {
                Name = dto.Name,
                YearOfBirth = dto.YearOfBirth,
                Email = dto.Email
            };

            _context.Authors.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }
    }
}