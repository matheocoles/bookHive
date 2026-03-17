using BookHive;
using BookHive.DTOs.Authors.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Authors;

public class GetAllAuthorsEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : EndpointWithoutRequest<List<GetAuthorDto>>
{
    public override void Configure()
    {
        Get("/authors/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var authors = await bookHiveDbContext.Authors.ToListAsync(ct);

        var response = mapper.Map<List<GetAuthorDto>>(authors);
        
        await Send.OkAsync(response, cancellation: ct);
    }
}