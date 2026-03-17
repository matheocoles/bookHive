using BookHive;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Member;

public class GetAllMemberEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :EndpointWithoutRequest<List<GetMemberDto>>
{
    public override void Configure()
    {
        Get("/users");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var members = await bookHiveDbContext.Members.ToListAsync(ct);

        var response = mapper.Map<List<GetMemberDto>>(members);
        
        await Send.OkAsync(response, cancellation: ct);
    }
}