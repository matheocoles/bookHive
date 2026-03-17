using BookHive.DTOs.Authors.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Authors;

public class GetAuthorsEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<GetAuthorDto>
{
    public override void Configure()
    {
        Get("/api/authors/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAuthorDto req, CancellationToken ct)
    {
        Author? author = await bookHiveDbContext
            .Authors
            .Include(a => a.Books)
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (author == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var responseDto = mapper.Map<GetAuthorDto>(author);

        await Send.OkAsync(responseDto, cancellation: ct);
    }
}