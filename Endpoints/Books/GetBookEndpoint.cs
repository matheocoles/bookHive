using AutoMapper;
using BookHive;
using BookHive.DTOs.Book.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Books;

public class GetBookEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<GetBookDto>
{
    public override void Configure()
    {
        Get("/api/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookDto req, CancellationToken ct)
    {
        var book = await bookHiveDbContext.Books
            .Include(b => b.Author) 
            .Include(b => b.Reviews) 
            .FirstOrDefaultAsync(b => b.Id == req.Id, ct);

        if (book == null) { await Send.NotFoundAsync(ct); return; }

        var response = mapper.Map<GetBookDto>(book);
        
        await Send.OkAsync(response, cancellation: ct);
    }
}