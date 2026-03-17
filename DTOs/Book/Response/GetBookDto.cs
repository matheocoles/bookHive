namespace BookHive.DTOs.Book.Response;

public class GetBookDto
{
    public int  Id { get; set; }
    public string? Title { get; set; }
    public string? Isbn { get; set; }
    public string? Summary { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishedDate { get; set; }
    public string? Genre { get; set; }
    
    public string AuthorFullName { get; set; } = string.Empty;
    
    public int? AuthorId { get; set; }
}