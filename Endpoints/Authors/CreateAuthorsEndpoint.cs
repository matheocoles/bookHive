using BookHive.DTOs.Authors.Request;
using BookHive.DTOs.Authors.Response;
using BookHive.Entities;
using FastEndpoints;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Authors;

public class CreateAuthorsEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<CreateAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Post("/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAuthorDto req, CancellationToken ct)
    {
        var author = mapper.Map<Author>(req);

        bookHiveDbContext.Authors.Add(author);
        await bookHiveDbContext.SaveChangesAsync(ct);

        var response = mapper.Map<GetAuthorDto>(author);
        await Send.OkAsync(response, ct);
    }
}