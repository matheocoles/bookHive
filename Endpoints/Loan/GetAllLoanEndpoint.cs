using BookHive;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Loan;

public class GetAllLoanEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :EndpointWithoutRequest<List<GetLoanDto>>
{
    public override void Configure()
    {
        Get("/loans");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var loans = await bookHiveDbContext.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .ToListAsync(ct);

        var response = mapper.Map<List<GetLoanDto>>(loans);
        await Send.OkAsync(response, cancellation: ct);
    }
}
