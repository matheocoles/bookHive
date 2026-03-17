using BookHive;
using BookHive.DTOs.Loan.Request;
using BookHive.DTOs.Loan.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Loan;

public class UpdateLoanEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) :Endpoint<UpdateLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Put("/loans/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateLoanDto req, CancellationToken ct)
    {
        var loan = await bookHiveDbContext.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.Id == req.Id, ct);

        if (loan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        mapper.Map(req, loan);

        await bookHiveDbContext.SaveChangesAsync(ct);

        var response = mapper.Map<GetLoanDto>(loan);
        
        await Send.OkAsync(response, cancellation: ct);
    }
}