using BookHive;
using BookHive.DTOs.Member.Request;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Member;

public class UpdateMemberEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :Endpoint<UpdateMemberDto, GetMemberDto>
{
    public override void Configure()
    {
        Put("/users/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateMemberDto req, CancellationToken ct)
    {
        var member = await bookHiveDbContext.Members.FindAsync([req.Id], ct);
        if (member == null) { await Send.NotFoundAsync(ct); return; }

        mapper.Map(req, member);

        await bookHiveDbContext.SaveChangesAsync(ct);
        await Send.OkAsync(mapper.Map<GetMemberDto>(member), cancellation: ct);
    }
}