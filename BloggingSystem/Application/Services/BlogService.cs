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
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogDto> CreateBlogAsync(BlogDto blogDto)
        {
            var blog = new Blog
            {
                Name = blogDto.Name,
                Url = blogDto.Url,
                AuthorId = blogDto.AuthorId
            };

            await _blogRepository.AddAsync(blog);
            return new BlogDto { Id = blog.Id, Name = blog.Name, Url = blog.Url, AuthorId = blog.AuthorId };
        }

        public async Task<List<BlogDto>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return blogs.Select(b => new BlogDto
            {
                Id = b.Id,
                Name = b.Name,
                Url = b.Url,
                AuthorId = b.AuthorId
            }).ToList();
        }

        public async Task<BlogDto> GetBlogByIdAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return null;
            return new BlogDto { Id = blog.Id, Name = blog.Name, AuthorId = blog.AuthorId };
        }

        public async Task<List<BlogDto>> GetBlogsByAuthorIdAsync(int authorId)
        {
            var blogs = await _blogRepository.GetBlogsByAuthorIdAsync(authorId);
            return blogs.Select(b => new BlogDto
            {
                Id = b.Id,
                Name = b.Name,
                Url = b.Url,
                AuthorId = b.AuthorId
            }).ToList();
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return false;
            _blogRepository.Remove(blog);
            return true;
        }
    }
}
