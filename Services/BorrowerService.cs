using LibraryApp.Data;
using LibraryApp.DTO;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services
{
    public class BorrowerService
    {
        private readonly LibraryAppContext _context;

        public BorrowerService(LibraryAppContext context)
        {
            _context = context;
        }

        public async Task<List<BorrowerDTO>> GetAllAsync()
        {
            return await _context.Borrowers
                .AsNoTracking()
                .Select(b => new BorrowerDTO
                {
                    Id = b.Id,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Email = b.Email,
                    PhoneNumber = b.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<BorrowerDTO?> GetByIdAsync(int id)
        {
            var borrower = await _context.Borrowers
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (borrower == null) return null;

            return new BorrowerDTO
            {
                Id = borrower.Id,
                FirstName = borrower.FirstName,
                LastName = borrower.LastName,
                Email = borrower.Email,
                PhoneNumber = borrower.PhoneNumber
            };
        }

        public async Task<BorrowerDTO> AddAsync(BorrowerDTO dto)
        {
            var borrower = new Borrower
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync();

            dto.Id = borrower.Id;
            return dto;
        }

        public async Task<BorrowerDTO?> UpdateAsync(int id, BorrowerDTO dto)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null) return null;

            borrower.FirstName = dto.FirstName;
            borrower.LastName = dto.LastName;
            borrower.Email = dto.Email;
            borrower.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null) return false;

            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}