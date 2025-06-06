
namespace BloggingSystem.Application.DTOs
{
    public class PostDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }

        public int BlogId { get; set; } 
    }
}
