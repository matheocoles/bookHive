namespace BookHive.Entities;

public class Book
{
    public int  Id { get; set; }
    public string? Title { get; set; }
    public string? Isbn { get; set; }
    public string? Summary { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishedDate { get; set; }
    public string? Genre { get; set; }
    
    public int? AuthorId { get; set; }
    public Author? Author { get; set; }
    public List<Loan>? Loans { get; set; }
}