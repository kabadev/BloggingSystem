using BloggingSystem.Domain.Interfaces;
using BloggingSystem.Infrastructure.Context;
using System;
using System.Threading.Tasks;

namespace BloggingSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BloggingDbContext _context;

        public IAuthorRepository Authors { get; }
        public IBlogRepository Blogs { get; }
        public IPostRepository Posts { get; }

        public UnitOfWork(BloggingDbContext context,
                          IAuthorRepository authorRepository,
                          IBlogRepository blogRepository,
                          IPostRepository postRepository)
        {
            _context = context;

            Authors = authorRepository;
            Blogs = blogRepository;
            Posts = postRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
