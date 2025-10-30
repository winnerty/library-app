using LibraryApp.Services;
using LibraryApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDto)
        {
            var created = await _bookService.AddAsync(bookDto);
            return CreatedAtAction(nameof(GetBook), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDTO bookDto)
        {
            var result = await _bookService.UpdateAsync(id, bookDto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}