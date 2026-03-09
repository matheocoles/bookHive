using BookHive.Data;
using BookHive.DTOs.Authors.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Authors;

public class GetAllAuthorsEndpoint(BookHiveDbContext bookHiveDbContext) : EndpointWithoutRequest<List<GetAuthorDto>>
{
    public override void Configure()
    {
        Get("/authors/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetAuthorDto> responseDto = await bookHiveDbContext.Authors
            .Select(a => new GetAuthorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Biography = a.Biography,
                    BirthDate = a.BirthDate,
                    Nationality = a.Nationality,

                }
            ).ToListAsync(ct);
        
        await Send.OkAsync(responseDto, ct);
    }
}