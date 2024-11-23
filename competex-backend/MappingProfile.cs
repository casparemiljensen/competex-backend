using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.Common.Helpers;
using competex_backend.Models;

namespace competex_backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDTO>();
            CreateMap<Club, ClubDTO>();
            CreateMap<Round, RoundDTO>();
            CreateMap<SportType, SportTypeDTO>();
            CreateMap<CompetitionType, CompetitionTypeDTO>();
            CreateMap<Competition, CompetitionDTO>();
            CreateMap<Event, EventDTO>();
            CreateMap<ClubMembership, ClubMembershipDTO>();
            CreateMap<Admin, AdminDTO>();
            CreateMap<Entity, EntityDTO>();
            CreateMap<Field, FieldDTO>();
            CreateMap<Location, LocationDTO>();
            CreateMap<Penalty, PenaltyDTO>();
            CreateMap<Registration, RegistrationDTO>();
            CreateMap<ScoringSystem, ScoringSystemDTO>();
            CreateMap<Judge, JudgeDTO>();
            CreateMap<Match, MatchDTO>();
            CreateMap<Participant, ParticipantDTO>()
                .Include<Team, TeamDetailDTO>()
                .Include<Models.Single, SingleDetailDTO>()
                .Include<Ekvipage, EkvipageDetailDTO>();
            //CreateMap<Team, TeamDetailDTO>()
            //    .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.MemberIds));
            CreateMap<Team, TeamDetailDTO>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom<TeamMembersResolver>());
            CreateMap<Models.Single, SingleDetailDTO>();
            CreateMap<Ekvipage, EkvipageDetailDTO>();



            CreateMap<MemberDTO, Member>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id during mapping
            CreateMap<ClubDTO, Club>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<RoundDTO, Round>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<SportTypeDTO, SportType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CompetitionTypeDTO, CompetitionType>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CompetitionDTO, Competition>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EventDTO, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ClubMembershipDTO, ClubMembership>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AdminDTO, Admin>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EntityDTO, Entity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<FieldDTO, Field>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<LocationDTO, Location>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PenaltyDTO, Penalty>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<RegistrationDTO, Registration>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ScoringSystemDTO, ScoringSystem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<JudgeDTO, Judge>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MatchDTO, Match>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ParticipantDTO, Participant>()
                .Include<TeamCreateUpdateDTO, Team>()
                .Include<SingleCreateUpdateDTO, Models.Single>()
                .Include<EkvipageCreateUpdateDTO, Ekvipage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TeamCreateUpdateDTO, Team>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<SingleCreateUpdateDTO, Models.Single>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EkvipageCreateUpdateDTO, Ekvipage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            // TODO: Use reversemap!
        }
    }
}
