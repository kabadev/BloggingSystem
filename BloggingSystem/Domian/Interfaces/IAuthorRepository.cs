using BloggingSystem.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BloggingSystem.Domain.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Blog>> GetBlogsByAuthorIdAsync(int authorId);
    }
}
