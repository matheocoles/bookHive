using BookHive;
using BookHive.DTOs.Review.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Review;

public class GetBookReviewsEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : EndpointWithoutRequest<List<GetReviewDto>>
{
    public override void Configure()
    {
        Get("/books/{bookId}/reviews");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int bookId = Route<int>("bookId");

        var reviews = await bookHiveDbContext.Reviews
            .Include(r => r.Member)
            .Where(r => r.BookId == bookId)
            .ToListAsync(ct);

        var response = mapper.Map<List<GetReviewDto>>(reviews);

        await Send.OkAsync(response, cancellation: ct);
    }
}