using BookHive.Data;
using BookHive.DTOs.Review.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Review;

public class GetBookReviewsEndpoint(BookHiveDbContext bookHiveDbContext) : EndpointWithoutRequest<List<GetReviewDto>>
{
    public override void Configure()
    {
        Get("/books/{bookId}/reviews"); // Route demandée 
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int bookId = Route<int>("bookId");

        var reviews = await bookHiveDbContext.Reviews
            .Where(r => r.BookId == bookId)
            .Select(r => new GetReviewDto
            {
                Id = r.Id,
                BookId = r.BookId,
                MemberId = r.MemberId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync(ct);

        await Send.OkAsync(reviews, cancellation: ct);
    }
}