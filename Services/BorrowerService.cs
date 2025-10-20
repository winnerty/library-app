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
                .Select(b => new BorrowerDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    Email = b.Email,
                    PhoneNumber = b.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<BorrowerDTO?> GetByIdAsync(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null) return null;

            return new BorrowerDTO
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Email = borrower.Email,
                PhoneNumber = borrower.PhoneNumber
            };
        }

        public async Task<BorrowerDTO> AddAsync(BorrowerDTO borrowerDto)
        {
            var borrower = new Borrower
            {
                Name = borrowerDto.Name,
                Email = borrowerDto.Email,
                PhoneNumber = borrowerDto.PhoneNumber
            };

            _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync();

            return new BorrowerDTO
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Email = borrower.Email,
                PhoneNumber = borrower.PhoneNumber
            };
        }

        public async Task<BorrowerDTO?> UpdateAsync(int id, BorrowerDTO borrowerDto)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null) return null;

            borrower.Name = borrowerDto.Name;
            borrower.Email = borrowerDto.Email;
            borrower.PhoneNumber = borrowerDto.PhoneNumber;

            await _context.SaveChangesAsync();

            return new BorrowerDTO
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Email = borrower.Email,
                PhoneNumber = borrower.PhoneNumber
            };
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