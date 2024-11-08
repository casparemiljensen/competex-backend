using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.Models;

namespace competex_backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClubMembership, ClubMembershipDTO>();

            ////Example of ignoring a property
            CreateMap<ClubMembershipDTO, ClubMembership>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore MemberId during mapping

            CreateMap<Member, MemberDTO>();
            CreateMap<Round, RoundDTO>();
            CreateMap<Competition, CompetitionDTO>();
            CreateMap<Event, EventDTO>();
            CreateMap<CompetitionType, CompetitionTypeDTO>();
            CreateMap<SportType, SportTypeDTO>();
            //CreateMap<MemberDTO, Member>();
            //CreateMap<MemberDTO, Member>().ConstructUsing(
            //        src => new Member(
            //                src.Id
            //            )
            //    );


            ////Example of ignoring a property
            CreateMap<MemberDTO, Member>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id during mapping
            CreateMap<RoundDTO, Round>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id during mapping
        }
    }
}
