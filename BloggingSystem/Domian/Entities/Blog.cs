

namespace BloggingSystem.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Url { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
