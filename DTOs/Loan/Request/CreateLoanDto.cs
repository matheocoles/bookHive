namespace BookHive.DTOs.Loan.Request;

public class CreateLoanDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime Date { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly ReturnDate { get; set; }
}