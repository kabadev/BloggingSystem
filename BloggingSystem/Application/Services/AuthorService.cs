using BloggingSystem.Application.DTOs;
using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingSystem.Application.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(a => new AuthorDto { Id = a.Id, Name = a.Name, Email = a.Email });
        }

     

        public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Email = authorDto.Email
            };

            await _authorRepository.AddAsync(author);
            return new AuthorDto { Id = author.Id, Name = author.Name, Email = author.Email };
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return null;

            return new AuthorDto { Id = author.Id, Name = author.Name, Email = author.Email };
        }
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return false;
            _authorRepository.Remove(author);
            return true;
        }
    }
}
