using Microsoft.AspNetCore.Mvc;
using BloggingSystem.Application.DTOs;
using BloggingSystem.Application.Services;
using System.Threading.Tasks;

namespace BloggingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] BlogDto blogDto)
        {
            var result = await _blogService.CreateBlogAsync(blogDto);
            return CreatedAtAction(nameof(GetAllBlogs), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var result = await _blogService.GetAllBlogsAsync();
            return Ok(result);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetBlogsByAuthor(int authorId)
        {
            var result = await _blogService.GetBlogsByAuthorIdAsync(authorId);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();
            return Ok(blog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blogService.DeleteBlogAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
