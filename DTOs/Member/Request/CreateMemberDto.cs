namespace BookHive.DTOs.Member.Request;

public class CreateMemberDto
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly MembershipDate { get; set; }
    public bool IsActive { get; set; }
}