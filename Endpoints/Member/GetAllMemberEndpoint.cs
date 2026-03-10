using BookHive.Data;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class GetAllMemberEndpoint(BookHiveDbContext bookHiveDbContext) :EndpointWithoutRequest<List<GetMemberDto>>
{
    public override void Configure()
    {
        Get("/users");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetMemberDto> responseDto = await bookHiveDbContext
            .Members
            .Select(u => new GetMemberDto
                {
                    Id = u.Id,
                    LastName = u.LastName,
                    FirstName = u.FirstName,
                    Email = u.Email,
                    MembershipDate = u.MembershipDate,
                    IsActive = u.IsActive
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}