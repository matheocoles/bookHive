using BookHive;
using BookHive.DTOs.Book.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Books;

public class DeleteBookEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetBookDto>
{
    public override void Configure()
    {
        Delete("/books/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetBookDto req, CancellationToken ct)
    {
        var book = await bookHiveDbContext
            .Books
            .Include(b => b.Loans)
            .FirstOrDefaultAsync(b => b.Id == req.Id, ct);
        if (book == null)
        {
            await Send.NotFoundAsync(ct); return;
        }

        if (book.Loans != null && book.Loans.Any(l => l.ReturnDate == null))
        {
            ThrowError("Impossible de supprimer un livre ayant un emprunt en cours.", 400);
            return;
        }
        
        Book? bookToDelete = await bookHiveDbContext
            .Books
            .SingleOrDefaultAsync(b => b.Id == req.Id, cancellationToken: ct);

        if (bookToDelete == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Books.Remove(bookToDelete);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}