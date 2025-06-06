using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using BloggingSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingSystem.Infrastructure.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly BloggingDbContext _context;

        public BlogRepository(BloggingDbContext context) : base(context)
        {
            _context = context;
        }

      

        public async Task<IEnumerable<Post>> GetPostsByBlogIdAsync(int blogId)
        {
            return await _context.Posts
                .Where(p => p.BlogId == blogId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetBlogsByAuthorIdAsync(int authorId)
        {
            return await _context.Blogs
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }
    }
}
