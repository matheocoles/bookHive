using BookHive;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Member;

public class GetMemberEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :Endpoint<GetMemberDto>
{
    public override void Configure()
    {
        Get("/users/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetMemberDto req, CancellationToken ct)
    {
        var member = await bookHiveDbContext.Members.FirstOrDefaultAsync(m => m.Id == req.Id, ct);
        if (member == null) { await Send.NotFoundAsync(ct); return; }
        await Send.OkAsync(mapper.Map<GetMemberDto>(member), cancellation: ct);
    }
}