using LibraryApp.Data;
using LibraryApp.DTO;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services
{
    public class ReservationService
    {
        private readonly LibraryAppContext _context;

        public ReservationService(LibraryAppContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationDTO>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.Borrower)
                .Include(r => r.Book)
                .Select(r => new ReservationDTO
                {
                    Id = r.Id,
                    ReservationDate = r.ReservationDate,
                    BorrowerId = r.BorrowerId,
                    BorrowerName = r.Borrower.Name,
                    BookId = r.BookId,
                    BookTitle = r.Book.Title,
                    ExpirationDate = r.ExpirationDate,
                    IsActive = r.IsActive
                })
                .ToListAsync();
        }

        public async Task<ReservationDTO?> GetByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Borrower)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return null;

            return new ReservationDTO
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                BorrowerId = reservation.BorrowerId,
                BorrowerName = reservation.Borrower.Name,
                BookId = reservation.BookId,
                BookTitle = reservation.Book.Title,
                ExpirationDate = reservation.ExpirationDate,
                IsActive = reservation.IsActive
            };
        }

        public async Task<ReservationDTO> AddAsync(ReservationDTO reservationDto)
        {
            var reservation = new Reservation
            {
                ReservationDate = reservationDto.ReservationDate,
                BorrowerId = reservationDto.BorrowerId,
                BookId = reservationDto.BookId,
                ExpirationDate = reservationDto.ExpirationDate,
                IsActive = true
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var createdReservation = await _context.Reservations
                .Include(r => r.Borrower)
                .Include(r => r.Book)
                .FirstAsync(r => r.Id == reservation.Id);

            return new ReservationDTO
            {
                Id = createdReservation.Id,
                ReservationDate = createdReservation.ReservationDate,
                BorrowerId = createdReservation.BorrowerId,
                BorrowerName = createdReservation.Borrower.Name,
                BookId = createdReservation.BookId,
                BookTitle = createdReservation.Book.Title,
                ExpirationDate = createdReservation.ExpirationDate,
                IsActive = createdReservation.IsActive
            };
        }

        public async Task<bool> UpdateAsync(int id, ReservationDTO reservationDto)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            reservation.ReservationDate = reservationDto.ReservationDate;
            reservation.BorrowerId = reservationDto.BorrowerId;
            reservation.BookId = reservationDto.BookId;
            reservation.ExpirationDate = reservationDto.ExpirationDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            reservation.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}