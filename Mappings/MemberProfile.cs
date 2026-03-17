using AutoMapper;
using BookHive.DTOs.Member.Request;
using BookHive.DTOs.Member.Response;
using BookHive.Entities;

namespace BookHive.Mappings;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, GetMemberDto>();
        CreateMap<CreateMemberDto, Member>();
        CreateMap<UpdateMemberDto, Member>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Email)));
    }
}