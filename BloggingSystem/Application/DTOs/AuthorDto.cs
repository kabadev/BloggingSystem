namespace BloggingSystem.Application.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; } // For reading, ignore during creation
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
