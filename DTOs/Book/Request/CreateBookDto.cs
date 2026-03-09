namespace BookHive.DTOs.Book.Request;

public class CreateBookDto
{
    public string? Title { get; set; }
    public string? Isbn { get; set; }
    public string? Summary { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishedDate { get; set; }
    public string? Genre { get; set; }
}