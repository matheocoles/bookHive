using BookHive.Data;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class GetAllLoanEndpoint(BookHiveDbContext bookHiveDbContext) :EndpointWithoutRequest<List<GetLoanDto>>
{
    public override void Configure()
    {
        Get("/loans");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetLoanDto> responseDto = await bookHiveDbContext
            .Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .Select(l => new GetLoanDto
            {
                Id = l.Id,
                Date = l.Date,
                BookId = l.BookId,
                MemberId = l.MemberId,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
            }).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}
