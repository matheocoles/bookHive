using BookHive;
using BookHive.DTOs.Loan.Request;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Loan;

public class PatchReturnLoanEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :Endpoint<PatchReturnLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Patch("/loans/{@id}/effectivereturn", x => new { x.Id });
    }

    public override async Task HandleAsync(PatchReturnLoanDto req, CancellationToken ct)
    {
        var loan = await bookHiveDbContext.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.Id == req.Id, ct);

        if (loan == null) { await Send.NotFoundAsync(ct); return; }

        loan.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetLoanDto>(loan), cancellation: ct);
    }
}
