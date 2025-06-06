using Microsoft.AspNetCore.Mvc;
using BloggingSystem.Application.DTOs;
using BloggingSystem.Application.Services;
using System.Threading.Tasks;

namespace BloggingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            var result = await _authorService.CreateAuthorAsync(authorDto);
            var successProperty = result.GetType().GetProperty("success");
            if (successProperty != null && !(bool)successProperty.GetValue(result))
            {
                return BadRequest(result); 
            }

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var result = await _authorService.GetAuthorByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            var success = (bool)result.GetType().GetProperty("success")?.GetValue(result);

            if (!success)
                return NotFound(result); 

            return Ok(result);
        }

    }
}
