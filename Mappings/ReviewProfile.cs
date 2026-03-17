using AutoMapper;
using BookHive.DTOs.Review.Request;
using BookHive.DTOs.Review.Response;
using BookHive.Entities;

namespace BookHive.Mappings;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, GetReviewDto>()
            .ForMember(dest => dest.MemberFullName, 
                opt => opt.MapFrom(src => $"{src.Member!.FirstName} {src.Member.LastName}"));
        
        CreateMap<CreateReviewDto, Review>();
    }
}