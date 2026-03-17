using BookHive;
using BookHive.DTOs.Book.Request;
using BookHive.DTOs.Book.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Books;

// TRequest = UpdateBookDto, TResponse = GetBookDto
public class UpdateBookEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<UpdateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Put("/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookDto req, CancellationToken ct)
    {
        var book = await bookHiveDbContext.Books.FindAsync([req.Id], ct);
        if (book == null) { await Send.NotFoundAsync(ct); return; }

        mapper.Map(req, book);

        await bookHiveDbContext.SaveChangesAsync(ct);
        await Send.OkAsync(mapper.Map<GetBookDto>(book), cancellation: ct);
    }
}