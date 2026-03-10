using BookHive.Data;
using BookHive.DTOs.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Books;

public class GetAllBooksEndpoint(BookHiveDbContext bookHiveDbContext) : EndpointWithoutRequest<List<GetBookDto>>
{
    public override void Configure()
    {
        Get("/books");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetBookDto> responseDto = await bookHiveDbContext.Books
            .Include(b => b.Author)
            .Select(b => new GetBookDto()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Isbn = b.Isbn,
                    Summary = b.Summary,
                    PageCount = b.PageCount,
                    PublishedDate =  b.PublishedDate,
                    Genre = b.Genre,
                    AuthorId = b.AuthorId,
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}