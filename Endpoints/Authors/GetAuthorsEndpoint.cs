using BookHive.Data;
using BookHive.DTOs.Authors.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Authors;

public class GetAuthorsEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetAuthorDto>
{
    public override void Configure()
    {
        Get("/authors/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetAuthorDto req, CancellationToken ct)
    {
        Author? author = await bookHiveDbContext
            .Authors
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (author == null)
        {
            Console.WriteLine($"Aucun author avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetAuthorDto responseDto = new()
        {
            Id = req.Id,
            LastName = author.LastName,
            FirstName = author.FirstName,
            BirthDate = author.BirthDate,
            Biography =  author.Biography,
            Nationality = author.Nationality,
        };

        await Send.OkAsync(responseDto, ct);
    }
}