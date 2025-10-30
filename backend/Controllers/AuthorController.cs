using LibraryApp.Services;
using LibraryApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor(AuthorDTO authorDto)
        {
            var created = await _authorService.AddAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDTO authorDto)
        {
            var result = await _authorService.UpdateAsync(id, authorDto);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}