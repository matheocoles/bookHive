using AutoMapper; // Ajout nécessaire
using BookHive.DTOs.Book.Request;
using BookHive.DTOs.Book.Response;
using BookHive.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace BookHive.Endpoints.Books;

public class CreateBookEndpoint(BookHiveDbContext bookHiveDbContext, IMapper mapper) : Endpoint<CreateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Post("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookDto req, CancellationToken ct)
    {
        var book = mapper.Map<Book>(req);
        
        bookHiveDbContext.Books.Add(book);
        await bookHiveDbContext.SaveChangesAsync(ct);

        var savedBook = await bookHiveDbContext.Books.Include(b => b.Author).FirstAsync(b => b.Id == book.Id, ct);
        
        await Send.OkAsync(mapper.Map<GetBookDto>(savedBook), ct);
    }
}