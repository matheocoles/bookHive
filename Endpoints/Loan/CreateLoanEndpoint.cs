using BookHive.Data;
using BookHive.DTOs.Loan.Request;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class CreateLoanEndpoint(BookHiveDbContext bookHiveDbContext) :Endpoint<CreateLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Post(("/loans"));
    }

    public override async Task HandleAsync(CreateLoanDto req, CancellationToken ct)
    {
        var members = await bookHiveDbContext.Members.FindAsync([req.MemberId], ct);
        if (members == null || !members.IsActive)
        {
            ThrowError("Un membre inactif ou inexistant ne peut pas emprunter de livre.", 403);
            return;
        }
        
        var isBookBorrowed = await bookHiveDbContext.Loans.AnyAsync(l => l.BookId == req.BookId && l.ReturnDate == null, ct);
        if (isBookBorrowed)
        {
            ThrowError("Ce livre est déjà en cours d'emprunt.", 400);
            return;
        }
        
        var daysDiff = req.DueDate.ToDateTime(TimeOnly.MinValue).Subtract(req.LoanDate.ToDateTime(TimeOnly.MinValue)).Days;
        if (daysDiff < 1 || daysDiff > 30)
        {
            ThrowError("La date de retour doit être comprise entre 1 et 30 jours après la date d'emprunt.", 400);
            return;
        }
        
        var book = await bookHiveDbContext.Books
            .FirstOrDefaultAsync(b => b.Id == req.BookId, ct);

        if (book == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        var member = await bookHiveDbContext.Members
            .SingleOrDefaultAsync(b => b.Id == req.MemberId, ct);

        if (member == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        var plannedReturningDate = req.Date.AddMonths(2);

        Entities.Loan loan = new()
        {
            Date = req.Date,
            LoanDate = req.LoanDate,
            DueDate =  req.DueDate,
            BookId = req.BookId,
            MemberId = member.Id,
        };


        bookHiveDbContext.Loans.Add(loan);
        await bookHiveDbContext.SaveChangesAsync(ct);
        

        var responseDto = new GetLoanDto
        {
            Id = loan.Id,
            Date = loan.Date,
            BookId = book.Id,
            MemberId = member.Id,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
        };

        await Send.OkAsync(responseDto, ct);
    }
}