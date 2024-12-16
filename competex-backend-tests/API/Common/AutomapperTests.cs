using AutoMapper;
using Common.ResultPattern;
using competex_backend;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace competex_backend_tests.API.Common
{
    public class AutomapperTests
    {
        private IMapper _mapper;
        public AutomapperTests() {

            var mockLocationService = new Mock<IGenericService<LocationDTO>>();
            mockLocationService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<LocationDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<LocationDTO>.Success(new LocationDTO { Id = id, Name = "Test Location" });
                });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Guid, LocationDTO>()
                   .ConvertUsing(new GuidToObjectConverter<LocationDTO>(mockLocationService.Object));
                cfg.CreateMap<Guid, LocationDTO>()
                   .ConvertUsing(new GuidToObjectConverter<LocationDTO>(mockLocationService.Object));
                cfg.CreateMap<Location, LocationDTO>();
                cfg.CreateMap<LocationDTO, Location>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<Round, RoundDTO>()
                    .ForMember(dest => dest.Competition, opt => opt.MapFrom(src => src.CompetitionId));
                cfg.CreateMap<Competition, CompetitionDTO>();
                cfg.CreateMap<Event, EventDTO>()
                    .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LocationId))
                    .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.OrganizerId))
                    .ForMember(dest => dest.SportType, opt => opt.MapFrom(src => src.SportTypeId))
                    .ForMember(dest => dest.Competitions, opt => opt.MapFrom(src => src.CompetitionIds));
            });

            _mapper = mapperConfig.CreateMapper();

            //mapperConfig.AssertConfigurationIsValid();
        }

        [Fact]
        public void GuidToObjectConverter_ConvertsGuidToLocationDTO()
        {
            var testGuid = Guid.NewGuid();

            // Act: Map a Guid to LocationDTO
            var location = _mapper.Map<LocationDTO>(testGuid);

            // Assert: Verify the mapping result
            Assert.NotNull(location);
            Assert.Equal(testGuid, location.Id);
            Assert.Equal("Test Location", location.Name);
        }

        [Fact]
        public void ModelToDTOConverter_ConvertsLocationToLocationDTO()
        {
            var location = new Location()
            {
                Name = "Test Location",
                Address = "Address",
                City = "City",
                Country = "Denamrk",
                Id = Guid.NewGuid(),
                Zip = "zip"
            };

            // Act: Map a Guid to LocationDTO
            var locationDTO = _mapper.Map<LocationDTO>(location);

            // Assert: Verify the mapping result
            Assert.NotNull(location);
            Assert.Equal(location.Id, locationDTO.Id);
            Assert.Equal(location.Name, locationDTO.Name);
            Assert.Equal(location.Address, locationDTO.Address);
            Assert.Equal(location.Zip, locationDTO.Zip);
            Assert.Equal(location.Country, locationDTO.Country);
        }

        [Fact]
        public void DTOToModelConverter_ConvertsLocationDTOToLocation()
        {
            var locationDTO = new LocationDTO()
            {
                Name = "Test Location",
                Address = "Address",
                City = "City",
                Country = "Denamrk",
                Id = Guid.NewGuid(),
                Zip = "zip"
            };

            // Act: Map a Guid to LocationDTO
            var location = _mapper.Map<Location>(locationDTO);

            // Assert: Verify the mapping result
            Assert.NotNull(location);
            Assert.NotEqual(location.Id, locationDTO.Id);
            Assert.Equal(location.Name, locationDTO.Name);
            Assert.Equal(location.Address, locationDTO.Address);
            Assert.Equal(location.Zip, locationDTO.Zip);
            Assert.Equal(location.Country, locationDTO.Country);
        }

        [Fact]
        public void ComplexModelToDTOConverter_ConvertsLocationToLocationDMO()
        {
            var sportType = new SportType()
            {
                EntityType = EntityType.Rabbit,
                Id = Guid.NewGuid(),
                Name = "Thumper"
            };
            var club = new Club()
            {
                Id = Guid.NewGuid(),
                AssociatedSport = "Tennis",
                Name = "KLG"
            };
            var location = new Location()
            {
                Name = "Test Location",
                Address = "Address",
                City = "City",
                Country = "Denamrk",
                Id = Guid.NewGuid(),
                Zip = "zip"
            };
            var member = new Member()
            {
                Id = Guid.NewGuid(),
                Birthday = new DateTime(),
                Email = "Email",
                FirstName = "Emil",
                LastName = "Hansen",
                Permissions = Convert.ToInt16(Permissions.Admin),
                Phone = "95830689"
            };
            var eventEntity = new Event()
            {
                Description = "desc",
                StartDate = new DateTime(),
                CompetitionIds = [],
                EndDate = new DateTime(),
                RegistrationEndDate = new DateTime(),
                EntryFee = 2000,
                RegistrationStartDate = new DateTime(),
                Id = Guid.NewGuid(),
                LocationId = location.Id,
                OrganizerId = club.Id,
                SportTypeId = sportType.Id,
                Status = Status.Active,
                Title = "Summer Tennis",
            };
            var competitionType = new CompetitionType()
            {
                Id = Guid.NewGuid(),
                Name = "CompetitionTypeName",
                ScoreMethod = ScoreMethod.D1,
                ScoreType = ScoreType.TimeAndPenalty,
            };
            var competition = new Competition() {
                CompetitionTypeId = competitionType.Id,
                Id = Guid.NewGuid(),
                EndDate = new DateTime(),
                StartDate = new DateTime(),
                EventId = eventEntity.Id,
                Level = Level.Intermediate,
                MaxParticipants = 10,
                MinParticipants = 5,
                Name = "U16",
                RegistrationPrice = 2000,
                Status = Status.Active,
            };
            var round = new Round()
            {
                Id = Guid.NewGuid(),
                Name = "Test Location",
                CompetitionId = competition.Id,
                EndTime = new DateTime(),
                RoundType = RoundType.Middle,
                SequenceNumber = 0,
                StartTime = new DateTime(),
                Status = RoundStatus.Starting,
            };

            var mockLocationService = new Mock<IGenericService<LocationDTO>>();
            mockLocationService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<LocationDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<LocationDTO>.Success(_mapper.Map<LocationDTO>(location));
                });

            var mockMemberService = new Mock<IGenericService<MemberDTO>>();
            mockMemberService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<MemberDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<MemberDTO>.Success(_mapper.Map<MemberDTO>(member));
                });
            var mockSportTypeService = new Mock<IGenericService<SportTypeDTO>>();
            mockSportTypeService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<SportTypeDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<SportTypeDTO>.Success(_mapper.Map<SportTypeDTO>(sportType));
                });
            var mockCompetitionService = new Mock<IGenericService<CompetitionDTO>>();
            mockCompetitionService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<CompetitionDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<CompetitionDTO>.Success(_mapper.Map<CompetitionDTO>(competition));
                });
            var mockCompetitionTypeService = new Mock<IGenericService<CompetitionTypeDTO>>();
            mockCompetitionTypeService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<CompetitionTypeDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<CompetitionTypeDTO>.Success(_mapper.Map<CompetitionTypeDTO>(competitionType));
                });
            var mockClubService = new Mock<IGenericService<ClubDTO>>();
            mockClubService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<ClubDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<ClubDTO>.Success(_mapper.Map<ClubDTO>(club));
                });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Guid, LocationDTO>()
                   .ConvertUsing(new GuidToObjectConverter<LocationDTO>(mockLocationService.Object));
                cfg.CreateMap<Guid, MemberDTO>()
                   .ConvertUsing(new GuidToObjectConverter<MemberDTO>(mockMemberService.Object));
                cfg.CreateMap<Guid, CompetitionTypeDTO>()
                   .ConvertUsing(new GuidToObjectConverter<CompetitionTypeDTO>(mockCompetitionTypeService.Object));
                cfg.CreateMap<Guid, CompetitionDTO>()
                   .ConvertUsing(new GuidToObjectConverter<CompetitionDTO>(mockCompetitionService.Object));
                cfg.CreateMap<Guid, ClubDTO>()
                   .ConvertUsing(new GuidToObjectConverter<ClubDTO>(mockClubService.Object));
                cfg.CreateMap<Guid, SportTypeDTO>()
                   .ConvertUsing(new GuidToObjectConverter<SportTypeDTO>(mockSportTypeService.Object));
                cfg.CreateMap<Club, ClubDTO>();
                cfg.CreateMap<Location, LocationDTO>();
                cfg.CreateMap<LocationDTO, Location>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<Round, RoundDTO>()
                    .ForMember(dest => dest.Competition, opt => opt.MapFrom(src => src.CompetitionId));
                cfg.CreateMap<Competition, CompetitionDTO>()
                    .ForMember(dest => dest.CompetitionType, opt => opt.MapFrom(src => src.CompetitionTypeId));
                cfg.CreateMap<CompetitionType, CompetitionTypeDTO>();
                cfg.CreateMap<Event, EventDTO>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LocationId))
                    .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.OrganizerId))
                    .ForMember(dest => dest.SportType, opt => opt.MapFrom(src => src.SportTypeId))
                    .ForMember(dest => dest.Competitions, opt => opt.MapFrom(src => src.CompetitionIds));
                cfg.CreateMap<SportType, SportTypeDTO>();
            });
            _mapper = mapperConfig.CreateMapper();
            
            _mapper.Map<SportTypeDTO>(sportType);
            _mapper.Map<CompetitionDTO>(competition);
            _mapper.Map<LocationDTO>(location);
            //_mapper.Map<RoundDTO>(round);
            _mapper.Map<ClubDTO>(club);
            // Act: Map a Guid to LocationDTO
            
            var eventDTO = _mapper.Map<EventDTO>(eventEntity);

            // Assert: Verify the mapping result
            Assert.NotNull(eventDTO);
            Assert.NotEqual(eventEntity.Id, eventDTO.Id);
            Assert.Equal(eventEntity.Title, eventDTO.Title);
            Assert.Equal(eventEntity.Status, eventDTO.Status);
            Assert.Equal(eventEntity.CompetitionIds, eventDTO.Competitions.Select(x => x.Id).ToList());
            Assert.Equal(eventEntity.LocationId, eventDTO.Location.Id);
        }

        [Fact]
        public void ComplexDTOToModelConverter_ConvertsLocationToLocationDMO()
        {
            var sportType = new SportTypeDTO()
            {
                EntityType = EntityType.Rabbit,
                Id = Guid.NewGuid(),
                Name = "Thumper"
            };
            var club = new ClubDTO()
            {
                Id = Guid.NewGuid(),
                AssociatedSport = "Tennis",
                Name = "KLG"
            };
            var location = new LocationDTO()
            {
                Name = "Test Location",
                Address = "Address",
                City = "City",
                Country = "Denamrk",
                Id = Guid.NewGuid(),
                Zip = "zip"
            };
            var member = new MemberDTO()
            {
                Id = Guid.NewGuid(),
                Birthday = new DateTime(),
                Email = "Email",
                FirstName = "Emil",
                LastName = "Hansen",
                Permissions = Convert.ToInt16(Permissions.Admin),
                Phone = "95830689"
            };
            var competitionType = new CompetitionTypeDTO()
            {
                Id = Guid.NewGuid(),
                Name = "CompetitionTypeName",
                ScoreMethod = ScoreMethod.D1,
                ScoreType = ScoreType.TimeAndPenalty,
            };
            var eventId = Guid.NewGuid();
            var competition = new CompetitionDTO()
            {
                CompetitionTypeId = competitionType.Id,
                Id = Guid.NewGuid(),
                EndDate = new DateTime(),
                StartDate = new DateTime(),
                EventId = eventId,
                Level = Level.Intermediate,
                MaxParticipants = 10,
                MinParticipants = 5,
                Name = "U16",
                RegistrationPrice = 2000,
                Status = Status.Active,
            };
            var eventDTO = new EventDTO()
            {
                Description = "desc",
                StartDate = new DateTime(),
                CompetitionIds = [ competition.Id ],
                EndDate = new DateTime(),
                RegistrationEndDate = new DateTime(),
                EntryFee = 2000,
                RegistrationStartDate = new DateTime(),
                Id = eventId,
                LocationId = location.Id,
                OrganizerId = club.Id,
                SportTypeId = sportType.Id,
                Status = Status.Active,
                Title = "Summer Tennis",
            };
            var round = new RoundDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Test Location",
                CompetitionId = competition.Id,
                Competition = competition,
                EndTime = new DateTime(),
                RoundType = RoundType.Middle,
                SequenceNumber = 0,
                StartTime = new DateTime(),
                Status = RoundStatus.Starting,
            };

            var mockLocationService = new Mock<IGenericService<LocationDTO>>();
            mockLocationService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<LocationDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<LocationDTO>.Success(_mapper.Map<LocationDTO>(location));
                });

            var mockMemberService = new Mock<IGenericService<MemberDTO>>();
            mockMemberService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<MemberDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<MemberDTO>.Success(_mapper.Map<MemberDTO>(member));
                });
            var mockSportTypeService = new Mock<IGenericService<SportTypeDTO>>();
            mockSportTypeService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<SportTypeDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<SportTypeDTO>.Success(_mapper.Map<SportTypeDTO>(sportType));
                });
            var mockCompetitionService = new Mock<IGenericService<CompetitionDTO>>();
            mockCompetitionService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<CompetitionDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<CompetitionDTO>.Success(_mapper.Map<CompetitionDTO>(competition));
                });
            var mockCompetitionTypeService = new Mock<IGenericService<CompetitionTypeDTO>>();
            mockCompetitionTypeService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<CompetitionTypeDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<CompetitionTypeDTO>.Success(_mapper.Map<CompetitionTypeDTO>(competitionType));
                });
            var mockClubService = new Mock<IGenericService<ClubDTO>>();
            mockClubService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<ClubDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<ClubDTO>.Success(_mapper.Map<ClubDTO>(club));
                });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MemberDTO, Member>()
.ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id during mapping
                cfg.CreateMap<ClubDTO, Club>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<RoundDTO, Round>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<SportTypeDTO, SportType>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<CompetitionTypeDTO, CompetitionType>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<CompetitionDTO, Competition>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<EventDTO, Event>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<ClubMembershipDTO, ClubMembership>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<AdminDTO, Admin>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<EntityDTO, Entity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<FieldDTO, Field>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<LocationDTO, Location>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<PenaltyDTO, Penalty>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<RegistrationDTO, Registration>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<JudgeDTO, Judge>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<TimeScoreDTO, TimeScore>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<SetScoreDTO, SetScore>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<PointScoreDTO, PointScore>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<TimeFaultScoreDTO, TimeFaultScore>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<ScoreResultDTO, ScoreResult>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
            });
            _mapper = mapperConfig.CreateMapper();

            _mapper.Map<SportType>(sportType);
            _mapper.Map<Competition>(competition);
            _mapper.Map<Location>(location);
            //_mapper.Map<RoundDTO>(round);
            _mapper.Map<Club>(club);
            // Act: Map a Guid to LocationDTO

            var eventEntity = _mapper.Map<Event>(eventDTO);

            // Assert: Verify the mapping result
            Assert.NotNull(eventEntity);
            Assert.Equal(eventEntity.Id, Guid.Empty);
            Assert.Equal(eventEntity.Title, eventDTO.Title);
            Assert.Equal(eventEntity.Status, eventDTO.Status);
            Assert.Equal(eventEntity.CompetitionIds, eventDTO.CompetitionIds);
            Assert.Equal(eventEntity.LocationId, eventDTO.LocationId);
        }

        [Fact]
        public void EmptyList_Mapping_DoesNotThrow()
        {
            // Arrange
            var emptyList = new List<Round>();

            // Act
            var result = _mapper.Map<List<RoundDTO>>(emptyList);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
