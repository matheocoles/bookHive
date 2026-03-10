using BookHive;
using BookHive.DTOs.Loan.Request;
using BookHive.DTOs.Loan.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class UpdateLoanEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<UpdateLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Put("/loans/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateLoanDto req, CancellationToken ct)
    {
        Entities.Loan? loanToEdit = await bookHiveDbContext
            .Loans
            .SingleOrDefaultAsync(l => l.Id == req.Id, cancellationToken: ct);

        if (loanToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        List<Book> book = await bookHiveDbContext
            .Books
            .Select(b => new Book { Id = b.Id, Title = b.Title })
            .ToListAsync();

        if (book == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        List<Entities.Member> user = await bookHiveDbContext
            .Members
            .Select(u => new Entities.Member { Id = u.Id, LastName = u.LastName, FirstName = u.FirstName })
            .ToListAsync();

        if (user == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        loanToEdit.Date = req.Date;
        loanToEdit.BookId = req.BookId;
        loanToEdit.MemberId = req.MemberId;
        loanToEdit.LoanDate = req.LoanDate;
        loanToEdit.DueDate = req.DueDate;

        await bookHiveDbContext.SaveChangesAsync(ct);

        GetLoanDto responseDto = new()
        {
            Id = req.Id,
            Date = req.Date,
            BookId = req.BookId,
            MemberId = req.MemberId,
            LoanDate = req.LoanDate,
            DueDate = req.DueDate,
        };

        await Send.OkAsync(responseDto, ct);
    }
}