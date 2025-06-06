using BloggingSystem.Application.DTOs;
using BloggingSystem.Domain.Entities;
using BloggingSystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingSystem.Application.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostDto> CreatePostAsync(PostDto postDto)
        {
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                DatePublished = postDto.DatePublished,
                BlogId = postDto.BlogId
            };

            await _postRepository.AddAsync(post);
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                DatePublished = post.DatePublished,
                BlogId = post.BlogId
            };
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(p => new PostDto { Id = p.Id, Title = p.Title, Content = p.Content, BlogId = p.BlogId });
        }


        public async Task<List<PostDto>> GetPostsByBlogIdAsync(int blogId)
        {
            // Get posts from repository (already awaited)
            var posts = await _postRepository.GetPostsByBlogIdAsync(blogId);

            // Map domain entities to DTOs and return as list
            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                DatePublished = p.DatePublished,
                BlogId = p.BlogId
            }).ToList();
        }


        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return false;
            _postRepository.Remove(post);
            return true;
        }
    }

}
