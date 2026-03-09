using BookHive.Data;
using BookHive.DTOs.Authors.Request;
using BookHive.DTOs.Authors.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Authors;

public class UpdateAuthorsEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<UpdateAuthorsDto, GetAuthorDto>
{
    public override void Configure()
    {
        Put("/authors/{@id}", x => new {x.Id});
    }

    public override async Task HandleAsync(UpdateAuthorsDto request, CancellationToken ct)
    {
        Author? authorToEdit = await bookHiveDbContext
            .Authors
            .SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken: ct);

        if (authorToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        authorToEdit.LastName = request.LastName;
        authorToEdit.FirstName = request.FirstName;
        authorToEdit.BirthDate = request.BirthDate;
        authorToEdit.Biography = request.Biography;
        authorToEdit.Nationality = request.Nationality;
        
        await bookHiveDbContext.SaveChangesAsync(ct);

        GetAuthorDto responseDto = new()
        {
            Id = request.Id,
            LastName = request.LastName,
            FirstName = request.FirstName,
            BirthDate = request.BirthDate,
            Biography = request.Biography,
            Nationality = request.Nationality,
        };
        
        await Send.OkAsync(responseDto, ct);
    }
}