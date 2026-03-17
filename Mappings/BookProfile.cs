using AutoMapper;
using BookHive.Entities;
using BookHive.DTOs.Book.Request;
using BookHive.DTOs.Book.Response;

namespace BookHive.Mappings;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, GetBookDto>()
            .ForMember(dest => dest.AuthorFullName, 
            opt => opt.MapFrom(src => $"{src.Author!.FirstName} {src.Author.LastName}"));

        CreateMap<CreateBookDto, Book>();

        CreateMap<UpdateBookDto, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            
    }
}