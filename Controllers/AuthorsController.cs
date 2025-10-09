using LibraryApp.Attributes;
using LibraryApp.Data;
using LibraryApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LibraryApp.DTO;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly AuthorsService _authorService;

        public AuthorsController(AuthorsService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorsDTO>>> Get()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorsDTO>> Get(int id)
        {
            //var query = context.Authors.Where(a => a.Id == id);
            //Console.WriteLine(query.ToQueryString());

            var author = await _authorService.GetByIdAsync(id);
            if (author is null) return NotFound();
            return Ok(author);
        }


        [HttpPost]
        public async Task<IActionResult> Post(AuthorsDTO author)
        {
            var created = await _authorService.AddAsync(author);
            return Ok(created);
        }
    }
}