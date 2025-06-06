using BloggingSystem.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BloggingSystem.Domain.Interfaces
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<IEnumerable<Post>> GetPostsByBlogIdAsync(int blogId);
        Task<IEnumerable<Blog>> GetBlogsByAuthorIdAsync(int authorId); 
    }
}
