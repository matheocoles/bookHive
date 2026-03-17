using BookHive;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Loan;

public class GetLoanEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :Endpoint<GetLoanDto>
{
    public override void Configure()
    {
        Get("/loans/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetLoanDto req, CancellationToken ct)
    {
        var loan = await bookHiveDbContext.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.Id == req.Id, ct);

        if (loan == null) { await Send.NotFoundAsync(ct); return; }

        await Send.OkAsync(mapper.Map<GetLoanDto>(loan), cancellation: ct);
    }
}