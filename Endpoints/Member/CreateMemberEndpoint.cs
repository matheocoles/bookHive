using BookHive;
using BookHive.DTOs.Member.Request;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using BookHive.Entities;


namespace BookHive.Endpoints.Member;

public class CreateMemberEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<CreateMemberDto, GetMemberDto>
{
    public override void Configure()
    {
        Post(("/members"));
    }

    public override async Task HandleAsync(CreateMemberDto req, CancellationToken ct)
    {
        Entities.Member member = new()
        {
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
            MembershipDate = req.MembershipDate,
            IsActive =  req.IsActive
        };

        bookHiveDbContext.Members.Add(member);
        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetMemberDto responseDto = new()
        {
            Id = member.Id,
            LastName = req.LastName,
            Email = req.Email,
            FirstName = req.FirstName,
            MembershipDate = req.MembershipDate,
            IsActive = req.IsActive
        };

        await Send.OkAsync(responseDto, ct);
    }
}