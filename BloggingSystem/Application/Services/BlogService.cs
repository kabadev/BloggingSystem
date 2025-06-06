using BloggingSystem.Application.DTOs;
using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using BloggingSystem.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingSystem.Application.Services
{
    public class BlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<object> CreateBlogAsync(BlogDto blogDto)
        {
            try { 
            var blog = new Blog
            {
                Name = blogDto.Name,
                Url = blogDto.Url,
                AuthorId = blogDto.AuthorId
            };

           
            await _unitOfWork.Blogs.AddAsync(blog);
            await _unitOfWork.CommitAsync();
            var saveBlog= new BlogDto { Id = blog.Id, Name = blog.Name, Url = blog.Url, AuthorId = blog.AuthorId };

            return new { success = true, message = "New blog added successfully", blog = saveBlog };
        }
            catch (Exception ex)
            {
                return new { success = false, message = "Error adding author", error = ex.Message
    };
}
            
        }



        public async Task<object> GetAllBlogsAsync()
        {
            try
            {
                var blogs = await _unitOfWork.Blogs.GetAllAsync();
                var result = blogs.Select(b => new BlogDto { Id = b.Id, Name = b.Name, AuthorId = b.AuthorId });

                return new { success = true, message = "Blogs retrieved successfully", data = result };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error retrieving blogs", error = ex.Message };
            }
        }


        public async Task<object> GetBlogByIdAsync(int id)
        {
            try
            {
                var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
                if (blog == null)
                    return new { success = false, message = "Blog not found" };

                var blogDto = new BlogDto { Id = blog.Id, Name = blog.Name, AuthorId = blog.AuthorId };
                return new { success = true, message = "Blog retrieved successfully", data = blogDto };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error retrieving blog", error = ex.Message };
            }
        }

        public async Task<object> GetBlogsByAuthorIdAsync(int authorId)
        
            {
            try { 
            var blogs = await _unitOfWork.Blogs.GetBlogsByAuthorIdAsync(authorId);
            var blogDtos = blogs.Select(b => new BlogDto
            {
                Id = b.Id,
                Name = b.Name,
                Url = b.Url,
                AuthorId = b.AuthorId
            }).ToList();

                return new { success = true, message = "Blogs retrieved successfully", blogs = blogDtos };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error retrieving blogs", error = ex.Message };
            }
        }

        public async Task<object> DeleteBlogAsync(int id)
        {
            try
            {
                var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
                if (blog == null)
                    return new { success = false, message = "Blog not found" };

                _unitOfWork.Blogs.Remove(blog);
                await _unitOfWork.CommitAsync();

                return new { success = true, message = "Blog deleted successfully" };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error deleting blog", error = ex.Message };
            }
        }

    }
}
