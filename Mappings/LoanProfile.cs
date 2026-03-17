using AutoMapper;
using BookHive.DTOs.Loan.Request;
using BookHive.DTOs.Loan.Response;
using BookHive.Entities;

namespace BookHive.Mappings;

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, GetLoanDto>()
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.MemberFullName, opt => opt.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"));
        CreateMap<CreateLoanDto, Loan>(); 
    }
}