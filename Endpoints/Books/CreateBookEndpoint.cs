using BookHive;
using BookHive.DTOs.Book.Request;
using BookHive.DTOs.Book.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Books;

public class CreateBookEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<CreateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Post("/books");
    }

    public override async Task HandleAsync(CreateBookDto req, CancellationToken ct)
    {
        var author = await bookHiveDbContext.Authors
            .FirstOrDefaultAsync(a => a.Id == req.AuthorId, ct);

        if (author == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        Book book = new()
        {
            Title = req.Title,
            Isbn = req.Isbn,
            Summary =  req.Summary,
            PageCount = req.PageCount,
            PublishedDate =  req.PublishedDate,
            Genre = req.Genre,
            AuthorId = req.AuthorId
        };


        bookHiveDbContext.Books.Add(book);
        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetBookDto responseDto = new()
        {
            Id = book.Id,
            Title = req.Title,
            Isbn = req.Isbn,
            Summary = req.Summary,
            PageCount = req.PageCount,
            PublishedDate = req.PublishedDate,
            Genre = req.Genre,
            AuthorId = req.AuthorId
        };

        await Send.OkAsync(responseDto, ct);
    }
}