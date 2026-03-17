using BookHive;
using BookHive.DTOs.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Books;

public class GetAllBooksEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : EndpointWithoutRequest<List<GetBookDto>>
{
    public override void Configure()
    {
        Get("/books");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var books = await bookHiveDbContext.Books
            .Include(b => b.Author)
            .ToListAsync(ct);

        var response = mapper.Map<List<GetBookDto>>(books);
        await Send.OkAsync(response, cancellation: ct);
    }
}