using LibraryApp.Services;
using LibraryApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly BorrowerService _borrowerService;

        public BorrowerController(BorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BorrowerDTO>>> GetBorrowers()
        {
            var borrowers = await _borrowerService.GetAllAsync();
            return Ok(borrowers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowerDTO>> GetBorrower(int id)
        {
            var borrower = await _borrowerService.GetByIdAsync(id);
            if (borrower == null) return NotFound();
            return Ok(borrower);
        }

        [HttpPost]
        public async Task<ActionResult<BorrowerDTO>> CreateBorrower(BorrowerDTO borrowerDto)
        {
            var created = await _borrowerService.AddAsync(borrowerDto);
            return CreatedAtAction(nameof(GetBorrower), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrower(int id, BorrowerDTO borrowerDto)
        {
            var result = await _borrowerService.UpdateAsync(id, borrowerDto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            var result = await _borrowerService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}