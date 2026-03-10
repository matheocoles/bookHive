using BookHive.Data;
using BookHive.DTOs.Member.Request;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class UpdateMemberEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<UpdateMemberDto, GetMemberDto>
{
    public override void Configure()
    {
        Put("/users/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateMemberDto req, CancellationToken ct)
    {
        Entities.Member? memberToEdit = await bookHiveDbContext
            .Members
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (memberToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        memberToEdit.LastName = req.LastName;
        memberToEdit.FirstName = req.FirstName;
        memberToEdit.Email = req.Email;
        memberToEdit.MembershipDate = req.MembershipDate;
        memberToEdit.IsActive = req.IsActive;

        await bookHiveDbContext.SaveChangesAsync(ct);

        GetMemberDto responseDto = new()
        {
            Id = req.Id,
            LastName = req.LastName,
            FirstName = req.FirstName,
            Email = req.Email,
            MembershipDate = req.MembershipDate,
            IsActive = req.IsActive,
        };

        await Send.OkAsync(responseDto, ct);
    }
}