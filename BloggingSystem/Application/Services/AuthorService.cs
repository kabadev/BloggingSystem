using BloggingSystem.Application.DTOs;
using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingSystem.Application.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<object> CreateAuthorAsync(AuthorDto authorDto)
        {
            try
            {
                var author = new Author { Name = authorDto.Name, Email = authorDto.Email };
                await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.CommitAsync();

                var savedAuthor = new AuthorDto { Id = author.Id, Name = author.Name, Email = author.Email };
                return new { success = true, message = "New author added successfully", author = savedAuthor };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error adding author", error = ex.Message };
            }
        }

        public async Task<object> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _unitOfWork.Authors.GetAllAsync();
                var result = authors.Select(a => new AuthorDto { Id = a.Id, Name = a.Name, Email = a.Email });

                return new { success = true, message = "Authors retrieved successfully", authors = result };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error retrieving authors", error = ex.Message };
            }
        }
        public async Task<object> GetAuthorByIdAsync(int id)
        {
            try
            {
                var author = await _unitOfWork.Authors.GetByIdAsync(id);
                if (author == null)
                    return new { success = false, message = "Author not found" };

                var authorDto = new AuthorDto { Id = author.Id, Name = author.Name, Email = author.Email };
                return new { success = true, message = "Author retrieved successfully", author = authorDto };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error retrieving author", error = ex.Message };
            }
        }
        public async Task<object> GetBlogsByAuthorIdAsync(int authorId)
        {
            try
            {
                var blogs = await _unitOfWork.Authors.GetBlogsByAuthorIdAsync(authorId);
                var blogDtos = blogs.Select(b => new BlogDto { Id = b.Id, Name = b.Name, AuthorId = b.AuthorId });

                return new { success = true, message = "Blogs retrieved successfully", data = blogDtos };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error retrieving blogs", error = ex.Message };
            }
        }

        public async Task<object> DeleteAuthorAsync(int id)
        {
            try
            {
                var author = await _unitOfWork.Authors.GetByIdAsync(id);
                if (author == null)
                    return new { success = false, message = "Author not found" };

                _unitOfWork.Authors.Remove(author);
                await _unitOfWork.CommitAsync();

                return new { success = true, message = "Author deleted successfully" };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error deleting author", error = ex.Message };
            }
        }

    }
}
