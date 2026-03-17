using BookHive;
using BookHive.DTOs.Review.Request;
using BookHive.DTOs.Review.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Review;

public class AddReviewEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<CreateReviewDto, GetReviewDto>
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

        var review = mapper.Map<Entities.Review>(req);
        review.BookId = bookId;

        bookHiveDbContext.Reviews.Add(review);
        await bookHiveDbContext.SaveChangesAsync(ct);

        var savedReview = await bookHiveDbContext.Reviews
            .Include(r => r.Member)
            .Include(r => r.Book)
            .FirstAsync(r => r.Id == review.Id, ct);

        await Send.OkAsync(mapper.Map<GetReviewDto>(savedReview), ct);
    }
}
