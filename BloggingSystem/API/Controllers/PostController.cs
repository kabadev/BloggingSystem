using Microsoft.AspNetCore.Mvc;
using BloggingSystem.Application.DTOs;
using BloggingSystem.Application.Services;
using System.Threading.Tasks;

namespace BloggingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDto postDto)
        {
            var result = await _postService.CreatePostAsync(postDto);
            //return CreatedAtAction(nameof(GetPostsByBlog), new { blogId = result.BlogId }, result);
            var successProperty = result.GetType().GetProperty("success");
            if (successProperty != null && !(bool)successProperty.GetValue(result))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }


        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetPostsByBlog(int blogId)
        {
            var result = await _postService.GetPostsByBlogIdAsync(blogId);
            return Ok(result);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            var success = (bool)result.GetType().GetProperty("success")?.GetValue(result);

            if (!success)
                return NotFound(result);

            return Ok(result);
        }
    }
}

