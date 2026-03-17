using BookHive;
using BookHive.DTOs.Loan.Request;
using BookHive.DTOs.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Loan;

public class CreateLoanEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :Endpoint<CreateLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Post(("/loans"));
    }

    public override async Task HandleAsync(CreateLoanDto req, CancellationToken ct)
    {
        var member = await bookHiveDbContext.Members.FindAsync([req.MemberId], ct);
        if (member == null || !member.IsActive)
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
        
        var daysDiff = req.DueDate.ToDateTime(TimeOnly.MinValue)
            .Subtract(req.LoanDate.ToDateTime(TimeOnly.MinValue)).Days;
            
        if (daysDiff < 1 || daysDiff > 30)
        {
            ThrowError("La date de retour doit être comprise entre 1 et 30 jours après la date d'emprunt.", 400);
            return;
        }

        var loan = mapper.Map<Entities.Loan>(req);

        bookHiveDbContext.Loans.Add(loan);
        await bookHiveDbContext.SaveChangesAsync(ct);
        
        var savedLoan = await bookHiveDbContext.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstAsync(l => l.Id == loan.Id, ct);

        var responseDto = mapper.Map<GetLoanDto>(savedLoan);

        await Send.OkAsync(responseDto, ct);
    }
}