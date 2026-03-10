using BookHive;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class GetMemberEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<GetMemberDto>
{
    public override void Configure()
    {
        Get("/users/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetMemberDto req, CancellationToken ct)
    {
        Entities.Member? user = await bookHiveDbContext
            .Members
            .FirstOrDefaultAsync(u => u.Id == req.Id, cancellationToken: ct);

        if (user == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        GetMemberDto responseDto = new()
        {
            Id = user.Id,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Email = user.Email,
            MembershipDate =  user.MembershipDate,
            IsActive =  user.IsActive,
        };

        await Send.OkAsync(responseDto, ct);
    }
}