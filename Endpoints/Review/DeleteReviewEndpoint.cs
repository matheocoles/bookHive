using BookHive.Data;
using BookHive.DTOs.Review.Response;
using FastEndpoints;

namespace BookHive.Endpoints.Review;

public class DeleteReviewEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetReviewDto>
{
    public override void Configure() => Delete("/api/reviews/{id}");

    public override async Task HandleAsync(GetReviewDto req, CancellationToken ct)
    {
        var review = await bookHiveDbContext.Reviews.FindAsync([req.Id], ct);

        if (review == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Reviews.Remove(review);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}
