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


        public async Task<BlogDto> CreateBlogAsync(BlogDto blogDto)
        {
            var blog = new Blog
            {
                Name = blogDto.Name,
                Url = blogDto.Url,
                AuthorId = blogDto.AuthorId
            };

           
            await _unitOfWork.Blogs.AddAsync(blog);
            await _unitOfWork.CommitAsync();
            return new BlogDto { Id = blog.Id, Name = blog.Name, Url = blog.Url, AuthorId = blog.AuthorId };
        }

       



        public async Task<IEnumerable<BlogDto>> GetAllBlogsAsync()
        {
            var blogs = await _unitOfWork.Blogs.GetAllAsync();
            return blogs.Select(b => new BlogDto { Id = b.Id, Name = b.Name, AuthorId = b.AuthorId });
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
            if (blog == null) return false;
            _unitOfWork.Blogs.Remove(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<BlogDto> GetBlogByIdAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
            if (blog == null) return null;
            return new BlogDto { Id = blog.Id, Name = blog.Name, AuthorId = blog.AuthorId };
        }

        public async Task<List<BlogDto>> GetBlogsByAuthorIdAsync(int authorId)
        {
            var blogs = await _unitOfWork.Blogs.GetBlogsByAuthorIdAsync(authorId);
            return blogs.Select(b => new BlogDto
            {
                Id = b.Id,
                Name = b.Name,
                Url = b.Url,
                AuthorId = b.AuthorId
            }).ToList();
        }


    }
}
