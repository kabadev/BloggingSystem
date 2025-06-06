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

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return authors.Select(a => new AuthorDto { Id = a.Id, Name = a.Name, Email = a.Email });
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null) return false;
            _unitOfWork.Authors.Remove(author);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
        {
            var author = new Author { Name = authorDto.Name, Email = authorDto.Email };
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CommitAsync();
            return new AuthorDto { Id = author.Id, Name = author.Name, Email = author.Email };
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null) return null;
            return new AuthorDto { Id = author.Id, Name = author.Name, Email = author.Email };
        }
    }
}
