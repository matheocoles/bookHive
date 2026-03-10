using BookHive.Data;
using BookHive.DTOs.Authors.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Authors;

public class DeleteAuthorsEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetAuthorDto>
{
    public override void Configure()
    {
        Delete("/authors/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetAuthorDto req, CancellationToken ct)
    {
        var author = await bookHiveDbContext
            .Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == req.Id, ct);
        if (author == null)
        {
            await Send.NotFoundAsync(ct); return;
        }

        if (author.Books.Any())
        {
            await Send.ErrorsAsync(409, ct);
            return;
        }
        
        Author? authorToDelete = await bookHiveDbContext
            .Authors
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (authorToDelete == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Authors.Remove(authorToDelete);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}