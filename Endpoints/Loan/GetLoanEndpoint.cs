using BookHive.Data;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class GetLoanEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<GetLoanDto>
{
    public override void Configure()
    {
        Get("/loans/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetLoanDto req, CancellationToken ct)
    {
        Entities.Loan? loan = await bookHiveDbContext
            .Loans
            .SingleOrDefaultAsync(l => l.Id == req.Id, cancellationToken: ct);

        if (loan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        GetLoanDto responseDto = new()
        {
            Id = req.Id,
            Date = loan.Date,
            BookId = loan.BookId,
            MemberId =  loan.MemberId,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
        };

        await Send.OkAsync(responseDto, ct);
    }
}