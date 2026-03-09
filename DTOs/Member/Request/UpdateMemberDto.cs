namespace BookHive.DTOs.Member.Request;

public class UpdateMemberDto
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly MembershipDate { get; set; }
    public bool IsActive { get; set; }
}