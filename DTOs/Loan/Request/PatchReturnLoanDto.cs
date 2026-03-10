namespace BookHive.DTOs.Loan.Request;

public class PatchReturnLoanDto
{
    public int Id { get; set; }
    public DateOnly ReturnDate { get; set; }
}