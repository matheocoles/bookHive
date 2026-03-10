using BookHive.Data;
using BookHive.DTOs.Authors.Response;
using BookHive.DTOs.Book.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Books;

public class GetBookEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetBookDto>
{
    public override void Configure()
    {
        Get("/books/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetBookDto req, CancellationToken ct)
    {
        Book? book = await bookHiveDbContext
            .Books
            .Include(b => b.Author)
            .SingleOrDefaultAsync(b => b.Id == req.Id, cancellationToken: ct);

        if (book == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        GetBookDto responseDto = new()
        {
            Id = req.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            Summary = book.Summary,
            PageCount = book.PageCount,
            PublishedDate = book.PublishedDate,
            Genre = book.Genre,
            AuthorId = book.AuthorId,
        };

        await Send.OkAsync(responseDto, ct);
    }
}