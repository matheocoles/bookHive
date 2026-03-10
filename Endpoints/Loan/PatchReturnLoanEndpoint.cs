using BookHive;
using BookHive.DTOs.Loan.Request;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class PatchReturnLoanEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<PatchReturnLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Patch("/loans/{@id}/effectivereturn", x => new { x.Id });
    }

    public override async Task HandleAsync(PatchReturnLoanDto req, CancellationToken ct)
    {


        Entities.Loan? loan = await bookHiveDbContext
            .Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .SingleOrDefaultAsync(l => l.Id == req.Id, ct);

        if (loan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (loan.ReturnDate != null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        loan.ReturnDate = req.ReturnDate;

        await bookHiveDbContext.SaveChangesAsync(ct);

        var response = new GetLoanDto
        {
            Id = loan.Id,
            Date = loan.Date,
            BookId = loan.BookId,
            MemberId = loan.MemberId,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnDate = loan.ReturnDate
        };

        await Send.OkAsync(response, ct);
    }
}
