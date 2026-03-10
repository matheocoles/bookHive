using BookHive.Data;
using BookHive.DTOs.Book.Request;
using BookHive.DTOs.Book.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Books;

// TRequest = UpdateBookDto, TResponse = GetBookDto
public class UpdateBookEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<UpdateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Put("/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookDto req, CancellationToken ct)
    {
        var bookToEdit = await bookHiveDbContext.Books
            .FirstOrDefaultAsync(b => b.Id == req.Id, ct);

        if (bookToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var authorExists = await bookHiveDbContext.Authors
            .AnyAsync(a => a.Id == req.AuthorId, ct);

        if (!authorExists)
        {
            return;
        }

        bookToEdit.Title = req.Title;
        bookToEdit.Isbn = req.Isbn;
        bookToEdit.AuthorId = req.AuthorId;
        bookToEdit.Summary = req.Summary;
        bookToEdit.PageCount = req.PageCount;
        bookToEdit.PublishedDate = req.PublishedDate;
        bookToEdit.Genre = req.Genre;

        await bookHiveDbContext.SaveChangesAsync(ct);

        var responseDto = new GetBookDto
        {
            Id = bookToEdit.Id,
            Title = bookToEdit.Title,
            Isbn = bookToEdit.Isbn,
            AuthorId = bookToEdit.AuthorId,
            Summary = bookToEdit.Summary,
            PageCount = bookToEdit.PageCount,
            PublishedDate = bookToEdit.PublishedDate,
            Genre = bookToEdit.Genre,
        };

        await Send.OkAsync(responseDto, cancellation: ct);
    }
}