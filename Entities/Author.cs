namespace BookHive.Entities;

public class Author
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Biography { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Nationality { get; set; }
    
    public List<Book>? Books { get; set; }
}