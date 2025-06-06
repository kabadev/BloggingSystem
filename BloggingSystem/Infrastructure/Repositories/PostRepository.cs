using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using BloggingSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BloggingSystem.Infrastructure.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly BloggingDbContext _context;

        public PostRepository(BloggingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPostsByBlogIdAsync(int blogId)
        {
            return await _context.Posts
                .Where(p => p.BlogId == blogId)
                .ToListAsync();
        }

    }
}
