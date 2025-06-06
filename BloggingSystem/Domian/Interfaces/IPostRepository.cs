using BloggingSystem.Domain.Entities;

namespace BloggingSystem.Domain.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByBlogIdAsync(int blogId);
    }
}
