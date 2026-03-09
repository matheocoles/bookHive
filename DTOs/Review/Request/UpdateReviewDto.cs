namespace BookHive.DTOs.Review.Request;

public class UpdateReviewDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}