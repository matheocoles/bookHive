using BookHive.Data;
using BookHive.DTOs.Review.Request;
using BookHive.DTOs.Review.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Review;

public class AddReviewEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<CreateReviewDto, GetReviewDto>
{
    public override void Configure() => Post("/books/{bookId}/reviews");

    public override async Task HandleAsync(CreateReviewDto req, CancellationToken ct)
    {
        int bookId = Route<int>("bookId");

        var member = await bookHiveDbContext.Members.FindAsync([req.MemberId], ct);
        if (member == null || !member.IsActive)
        {
            ThrowError("Un membre inactif ne peut pas laisser d'avis.", 403);
            return;
        }

        var alreadyExists = await bookHiveDbContext.Reviews.AnyAsync(r => r.BookId == bookId && r.MemberId == req.MemberId, ct);
        if (alreadyExists)
        {
            ThrowError("Un membre ne peut laisser qu'un seul avis par livre.", 400);
            return;
        }

        var review = new Entities.Review
        {
            BookId = bookId,
            MemberId = req.MemberId,
            Rating = req.Rating,
            Comment = req.Comment,
        };

        bookHiveDbContext.Reviews.Add(review);
        await bookHiveDbContext.SaveChangesAsync(ct);

        var response = new GetReviewDto
        {
            Id = review.Id,
            BookId = review.BookId,
            MemberId = review.MemberId,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt
        };

        await Send.OkAsync(response, ct);
    }
}
