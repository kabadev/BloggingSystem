using Microsoft.Extensions.DependencyInjection;
using BloggingSystem.Domain.Interfaces;
using BloggingSystem.Infrastructure.Repositories;
using BloggingSystem.Application.Services;

namespace BloggingSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            // Register Services
            services.AddScoped<AuthorService>();
            services.AddScoped<BlogService>();
            services.AddScoped<PostService>();

            return services;
        }
    }
}
