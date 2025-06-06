namespace BloggingSystem.Application.DTOs
{
    public class BlogDto
    {
        public int Id { get; set; } // Optional in create/update
        public string Name { get; set; }
        public string Url { get; set; }

        public int AuthorId { get; set; } // Foreign key
    }
}
