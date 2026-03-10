using BookHive;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class DeleteLoanEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<GetLoanDto>
{
    public override void Configure()
    {
        Delete("/loans/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetLoanDto req, CancellationToken ct)
    {
        var loanToDelete = await bookHiveDbContext
            .Loans
            .SingleOrDefaultAsync(l => l.Id == req.Id, cancellationToken: ct);

        if (loanToDelete == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Loans.Remove(loanToDelete);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}