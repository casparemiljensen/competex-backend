using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Common.ErrorHandling;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Reflection.Metadata.Ecma335;

namespace competex_backend.BLL.Services
{
    public class EventService : GenericService<Event, EventDTO>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly ICompetitionRepository _competitionRepository;


        public EventService(IGenericRepository<Event> repository, IMapper mapper, IRegistrationRepository registrationRepository, ICompetitionRepository competitionRepository)
            : base(repository, mapper)
        {
            _eventRepository = (IEventRepository)repository;
            _mapper = mapper;
            _registrationRepository = registrationRepository;
            _competitionRepository = competitionRepository;
        }

        public async Task<Result> AddCompetition(Guid eventId, Guid competitionId)
        {
            var competitionResult = await _competitionRepository.GetByIdAsync(competitionId);
            if (!competitionResult.IsSuccess)
            {
                return Result.Failure(competitionResult.Error!);
            }

            var result = await _eventRepository.AddCompetition(eventId, competitionResult.Value);
            if (!result.IsSuccess)
            {
                return Result.Failure(result.Error!);
            }
            return result;
        }

        public async Task<ResultT<int>> GetMembersOwedAmount(Guid memberId, Guid eventId)
        {
            //var selectedEvent = await _eventRepository.GetByIdAsync(eventId);

            var selectedEvent = await GetByIdAsync(eventId);

            if (!selectedEvent.IsSuccess)
            {
                return ResultT<int>.Failure(selectedEvent.Error!);
            }
            //.ToList() is necessary to force LinQ query to evaluate
            var searchParams = new Dictionary<string, object>()
            {
                { "MemberId", memberId },
                { "CompetitionId", selectedEvent.Value.Competitions.Select(competition => competition.Id).ToList() }
            };

            var registrations = await PaginationHelper.GetAll<Registration, IRegistrationRepository>(_registrationRepository, searchParams);

            Dictionary<Guid, int> competitionPrices = [];

            foreach (var competition in selectedEvent.Value.Competitions!)
            {
                competitionPrices[competition.Id] = competition.RegistrationPrice;
            }

            return registrations.Aggregate(0, (sum, registration) => sum + competitionPrices[registration.Competition.Id]) + selectedEvent.Value.EntryFee;
        }
    }
}