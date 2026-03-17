using BookHive;
using BookHive.DTOs.Member.Request;
using BookHive.DTOs.Member.Response;
using FastEndpoints;
using BookHive.Entities;
using IMapper = AutoMapper.IMapper;


namespace BookHive.Endpoints.Member;

public class CreateMemberEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<CreateMemberDto, GetMemberDto>
{
    public override void Configure()
    {
        Post(("/members"));
    }

    public override async Task HandleAsync(CreateMemberDto req, CancellationToken ct)
    {
        var member = mapper.Map<Entities.Member>(req);
        bookHiveDbContext.Members.Add(member);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetMemberDto>(member), ct);
    }
}