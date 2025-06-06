using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using BloggingSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingSystem.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly BloggingDbContext _context;

        public AuthorRepository(BloggingDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Blog>> GetBlogsByAuthorIdAsync(int authorId)
        {
            return await _context.Blogs.Where(b => b.AuthorId == authorId).ToListAsync();
        }
        

    }
}
