using AutoMapper;
using BookHive.Entities;
using BookHive.DTOs.Authors.Request;
using BookHive.DTOs.Authors.Response;

namespace BookHive.Mappings;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, GetLightAuthorDto>();

        CreateMap<Author, GetAuthorDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

        CreateMap<CreateAuthorDto, Author>();

        CreateMap<UpdateAuthorsDto, Author>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}