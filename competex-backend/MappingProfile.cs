using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.Models;

namespace competex_backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDTO>();

            CreateMap<MemberDTO, Member>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore MemberId during mapping
        }
    }
}
