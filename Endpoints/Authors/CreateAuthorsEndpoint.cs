using BookHive;
using BookHive.DTOs.Authors.Request;
using BookHive.DTOs.Authors.Response;
using BookHive.Entities;
using FastEndpoints;

namespace BookHive.Endpoints.Authors;

public class CreateAuthorsEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<CreateAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Post("/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAuthorDto req, CancellationToken ct)
    {
        Author author = new()
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Biography = req.Biography,
            BirthDate = req.BirthDate,
            Nationality = req.Nationality,
        };
        
        bookHiveDbContext.Authors.Add(author);
        await bookHiveDbContext.SaveChangesAsync(ct);

        GetAuthorDto responseDto = new()
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography,
            BirthDate = author.BirthDate,
            Nationality = author.Nationality,
        };
        
        await Send.OkAsync(responseDto, ct);
    }
}