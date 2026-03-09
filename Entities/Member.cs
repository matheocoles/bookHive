namespace BookHive.Entities;

public class Member
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly MembershipDate { get; set; }
    public bool IsActive { get; set; }
    
    public List<Loan>? Loans { get; set; }
    public List<Review>? Reviews { get; set; }
}