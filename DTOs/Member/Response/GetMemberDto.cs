namespace BookHive.DTOs.Member.Response;

public class GetMemberDto
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly MembershipDate { get; set; }
    public bool IsActive { get; set; }
}