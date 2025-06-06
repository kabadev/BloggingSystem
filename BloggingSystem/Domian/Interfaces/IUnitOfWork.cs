using System.Threading.Tasks;

namespace BloggingSystem.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IBlogRepository Blogs { get; }
        IPostRepository Posts { get; }

        Task<int> CommitAsync();
    }
}
