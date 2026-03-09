namespace BookHive.DTOs.Book.Response;

public class GetBookDto
{
    public int  Id { get; set; }
    public string? Title { get; set; }
    public string? Isbn { get; set; }
    public string? Genre { get; set; }
}