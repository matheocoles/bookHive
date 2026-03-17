using BookHive;
using BookHive.DTOs.Authors.Request;
using BookHive.DTOs.Authors.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Authors;

public class UpdateAuthorsEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<UpdateAuthorsDto, GetAuthorDto>
{
    public override void Configure()
    {
        Put("/authors/{@id}", x => new {x.Id});
    }

    public override async Task HandleAsync(UpdateAuthorsDto req, CancellationToken ct)
    {
        var author = await bookHiveDbContext.Authors.FindAsync([req.Id], ct);
        if (author == null) { await Send.NotFoundAsync(ct); return; }

        mapper.Map(req, author);

        await bookHiveDbContext.SaveChangesAsync(ct);

        var response = mapper.Map<GetAuthorDto>(author);
        await Send.OkAsync(response, cancellation: ct);
    }
}