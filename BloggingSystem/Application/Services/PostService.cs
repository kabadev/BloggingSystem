using BloggingSystem.Application.DTOs;
using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingSystem.Application.Services
{
    public class PostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> CreatePostAsync(PostDto postDto)
        {
            try
            {
                var post = new Post
                {
                    Title = postDto.Title,
                    Content = postDto.Content,
                    DatePublished = postDto.DatePublished,
                    BlogId = postDto.BlogId
                };

                await _unitOfWork.Posts.AddAsync(post);
                await _unitOfWork.CommitAsync();
                var savePostDto = new PostDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    DatePublished = post.DatePublished,
                    BlogId = post.BlogId
                };
                return new { success = true, message = "New post added successfully", post = savePostDto };

            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error adding post", error = ex.Message };
            }

        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _unitOfWork.Posts.GetAllAsync();
            return posts.Select(p => new PostDto { Id = p.Id, Title = p.Title, Content = p.Content, BlogId = p.BlogId });
        }


        public async Task<object> GetPostsByBlogIdAsync(int blogId)
        {
            try
            {
                var posts = await _unitOfWork.Posts.GetPostsByBlogIdAsync(blogId);


                var postData = posts.Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    DatePublished = p.DatePublished,
                    BlogId = p.BlogId
                }).ToList();
                return new { success = true, message = "posts retrieved successfully", data = postData };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error retrieving blogs", error = ex.Message };
            }
        }


        public async Task<object> DeletePostAsync(int id)
        {
            try
            {
                var post = await _unitOfWork.Posts.GetByIdAsync(id);
                if (post == null) return false;
                _unitOfWork.Posts.Remove(post);
                await _unitOfWork.CommitAsync();


                return new { success = true, message = "Post deleted successfully" };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error deleting blog", error = ex.Message };
            }

        }
    }
}


