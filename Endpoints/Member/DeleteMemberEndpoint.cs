using BookHive;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class DeleteMemberEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<GetMemberDto>
{
    public override void Configure()
    {
        Delete("/users/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetMemberDto req, CancellationToken ct)
    {
        Entities.Member? memberToDelete = await bookHiveDbContext
            .Members
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (memberToDelete == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Members.Remove(memberToDelete);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}