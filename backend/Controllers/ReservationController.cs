using LibraryApp.Services;
using LibraryApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationDTO>>> GetReservations()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> CreateReservation(ReservationDTO reservationDto)
        {
            var created = await _reservationService.AddAsync(reservationDto);
            return CreatedAtAction(nameof(GetReservation), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationDTO reservationDto)
        {
            var result = await _reservationService.UpdateAsync(id, reservationDto);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var result = await _reservationService.CancelReservationAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}