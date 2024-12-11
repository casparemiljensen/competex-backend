using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.Models;

namespace competex_backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guid, MemberDTO>().ConvertUsing<GuidToObjectConverter<MemberDTO>>();
            CreateMap<Guid, ClubDTO>().ConvertUsing<GuidToObjectConverter<ClubDTO>>();
            CreateMap<Guid, RoundDTO>().ConvertUsing<GuidToObjectConverter<RoundDTO>>();
            CreateMap<Guid, SportTypeDTO>().ConvertUsing<GuidToObjectConverter<SportTypeDTO>>();
            CreateMap<Guid, CompetitionTypeDTO>().ConvertUsing<GuidToObjectConverter<CompetitionTypeDTO>>();
            CreateMap<Guid, CompetitionDTO>().ConvertUsing<GuidToObjectConverter<CompetitionDTO>>();
            CreateMap<Guid, EventDTO>().ConvertUsing<GuidToObjectConverter<EventDTO>>();
            CreateMap<Guid, ClubMembershipDTO>().ConvertUsing<GuidToObjectConverter<ClubMembershipDTO>>();
            CreateMap<Guid, AdminDTO>().ConvertUsing<GuidToObjectConverter<AdminDTO>>();
            CreateMap<Guid, EntityDTO>().ConvertUsing<GuidToObjectConverter<EntityDTO>>();
            CreateMap<Guid, FieldDTO>().ConvertUsing<GuidToObjectConverter<FieldDTO>>();
            CreateMap<Guid, LocationDTO>().ConvertUsing<GuidToObjectConverter<LocationDTO>>();
            CreateMap<Guid, PenaltyDTO>().ConvertUsing<GuidToObjectConverter<PenaltyDTO>>();
            CreateMap<Guid, RegistrationDTO>().ConvertUsing<GuidToObjectConverter<RegistrationDTO>>();
            CreateMap<Guid, ScoringSystemDTO>().ConvertUsing<GuidToObjectConverter<ScoringSystemDTO>>();
            CreateMap<Guid, JudgeDTO>().ConvertUsing<GuidToObjectConverter<JudgeDTO>>();
            CreateMap<Guid, MatchDTO>().ConvertUsing<GuidToObjectConverter<MatchDTO>>();
            CreateMap<Guid, ParticipantDTO>().ConvertUsing<GuidToObjectConverter<ParticipantDTO>>();
            CreateMap<Guid, ScoreDTO>().ConvertUsing<GuidToObjectConverter<ScoreDTO>>();
            CreateMap<Guid, ScoreResultDTO>().ConvertUsing<GuidToObjectConverter<ScoreResultDTO>>();



            CreateMap<Member, MemberDTO>();
            CreateMap<Club, ClubDTO>();
            CreateMap<Round, RoundDTO>()
                .ForMember(dest => dest.Competition, opt => opt.MapFrom(src => src.CompetitionId));
            CreateMap<SportType, SportTypeDTO>();
            CreateMap<CompetitionType, CompetitionTypeDTO>();
            CreateMap<Competition, CompetitionDTO>()
                .ForMember(dest => dest.CompetitionType, opt => opt.MapFrom(src => src.CompetitionTypeId));
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LocationId))
                .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.OrganizerId))
                .ForMember(dest => dest.SportType, opt => opt.MapFrom(src => src.SportTypeId))
                .ForMember(dest => dest.Competitions, opt => opt.MapFrom(src => src.CompetitionIds));
            CreateMap<ClubMembership, ClubMembershipDTO>();
            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.SportTypes, opt => opt.MapFrom(src => src.SportTypeIds));
            CreateMap<Entity, EntityDTO>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.OwnerId));
            CreateMap<Field, FieldDTO>();
            CreateMap<Location, LocationDTO>();
            CreateMap<Penalty, PenaltyDTO>();
            CreateMap<Registration, RegistrationDTO>()
                .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.ParticipantId))
                .ForMember(dest => dest.Competition, opt => opt.MapFrom(src => src.CompetitionId));
            CreateMap<ScoringSystem, ScoringSystemDTO>() // Cannot map penalties somehow.
                .ForMember(dest => dest.Penalties, opt => opt.MapFrom(src => src.PenaltyIds));
            CreateMap<Judge, JudgeDTO>()
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src.MemberId));
            CreateMap<Match, MatchDTO>()
                .ForMember(dest => dest.Round, opt => opt.MapFrom(src => src.RoundId))
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.ParticipantIds))
                .ForMember(dest => dest.Field, opt => opt.MapFrom(src => src.FieldId))
                .ForMember(dest => dest.Judge, opt => opt.MapFrom(src => src.JudgeId));
                //.ForMember(dest => dest.Scores, opt => opt.MapFrom(src => src.ScoreIds));
            CreateMap<Participant, ParticipantDTO>()
                .Include<Team, TeamDTO>()
                .Include<Models.Single, SingleDTO>()
                .Include<Ekvipage, EkvipageDTO>();
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.MemberIds));
            CreateMap<Models.Single, SingleDTO>()
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src.MemberId));
            CreateMap<Ekvipage, EkvipageDTO>()
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.EntityId));
            CreateMap<Score, ScoreDTO>()
                //.ForMember(dest => dest.Match, opt => opt.MapFrom(src => src.MatchId))
                //.ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.ParticipantId))
                .ForMember(dest => dest.ScoreValue, opt => opt.MapFrom(src => src.ScoreValue)) // Do not know how to handle this...
                .ForMember(dest => dest.Penalties, opt => opt.MapFrom(src => src.PenaltyIds))
                .Include<TimeScore, TimeScoreDTO>()
                .Include<SetScore, SetScoreDTO>()
                .Include<PointScore, PointScoreDTO>()
                .Include<TimeFaultScore, TimeFaultScoreDTO>();
            CreateMap<TimeScore, TimeScoreDTO>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.ScoreValue));
            CreateMap<SetScore, SetScoreDTO>()
                .ForMember(dest => dest.SetsWon, opt => opt.MapFrom(src => src.ScoreValue));
            CreateMap<PointScore, PointScoreDTO>()
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.ScoreValue));
            CreateMap<TimeFaultScore, TimeFaultScoreDTO>();
            //.ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time))
            //.ForMember(dest => dest.Faults, opt => opt.MapFrom(src => src.Faults));
            CreateMap<ScoreResult, ScoreResultDTO>();
                //.ForMember(dest => dest.Competition, opt => opt.MapFrom(src => src.CompetitionId))
                //.ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.ParticipantId));


            // Reverse mappings
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
                .Include<TeamDTO, Team>()
                .Include<SingleDTO, Models.Single>()
                .Include<EkvipageDTO, Ekvipage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TeamDTO, Team>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<SingleDTO, Models.Single>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EkvipageDTO, Ekvipage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ScoreDTO, Score>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .Include<TimeScoreDTO, TimeScore>()
                .Include<SetScoreDTO, SetScore>()
                .Include<PointScoreDTO, PointScore>()
                .Include<TimeFaultScoreDTO, TimeFaultScore>();
            CreateMap<TimeScoreDTO, TimeScore>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<SetScoreDTO, SetScore>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PointScoreDTO, PointScore>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TimeFaultScoreDTO, TimeFaultScore>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ScoreResultDTO, ScoreResult>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }


        public class GuidToObjectConverter<T> : ITypeConverter<Guid, T> where T : class
        {
            private readonly IGenericService<T> _service;

            public GuidToObjectConverter(IGenericService<T> service)
            {
                _service = service;
            }

            public T Convert(Guid source, T destination, ResolutionContext context)
            {
                if (source == Guid.Empty)
                {
                    Console.WriteLine($"Empty Guid received for {typeof(T).Name}");
                    return default; // Return null for reference types
                }

                var result = _service.GetByIdAsync(source).GetAwaiter().GetResult();
                if (result == null || !result.IsSuccess)
                {
                    Console.WriteLine($"Failed to resolve {typeof(T).Name} for Guid: {source}");
                    return default;
                }

                Console.WriteLine($"Successfully resolved {typeof(T).Name} for Guid: {source}");
                return result.Value;
            }
        }
    }
}
