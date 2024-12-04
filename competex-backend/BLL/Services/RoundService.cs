using AutoMapper;
using Common.ResultPattern;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Linq;

namespace competex_backend.BLL.Services
{
    public class RoundService : GenericService<Round, RoundDTO>, IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IMapper _mapper;
        private readonly IRegistrationService _registrationService;
        private readonly IScoreService _scoreService;
        private readonly IMatchService _matchService;

        public RoundService(IGenericRepository<Round> repository, IMapper mapper, IRegistrationService registrationService, IScoreService scoreService, IMatchService matchService) : base(repository, mapper)
        {
            _roundRepository = (IRoundRepository)repository;
            _mapper = mapper;
            _registrationService = registrationService;
            _scoreService = scoreService;
            _matchService = matchService;
        }

        public async Task<ResultT<Tuple<int, IEnumerable<RoundDTO>>>> GetByCompetitionId(Guid competitionId, int? pageSize, int? pageNumber)
        {
            var result = await _roundRepository.GetRoundIdsByCompetitionId(competitionId, pageSize, pageNumber);
            if (result.IsSuccess && result.Value != null)
            {
                return ResultT<Tuple<int, IEnumerable<RoundDTO>>>.Success(new Tuple<int, IEnumerable<RoundDTO>>(result.Value.Item1, result.Value.Item2.Select(round => _mapper.Map<RoundDTO>(round))));
            }
            return ResultT<Tuple<int, IEnumerable<RoundDTO>>>.Failure(result.Error ?? Error.Failure("UnknownError", "An unknown error occurred."));
        }


        public async Task<ResultT<Tuple<int, IEnumerable<MatchDTO>>>> CreateMatchesForRoundAsync(Guid competitionId, uint roundSequenceNumber, int? pageSize, int? pageNumber)
        {
            //We might actually need domains models here but services layers returns DTOS...
            //Fictional Criteria, make it optional
            // I use services some places, might need repositories instead

            Func<(int Fault, TimeSpan Time), bool> timeFaultCriteria = tuple =>
                tuple.Fault < 3 && tuple.Time.TotalMinutes <= 3;


            var registrationsResult = await _registrationService.GetAllAsync(null, null);

            if (!registrationsResult.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(registrationsResult.Error!);
            }

            // Get all registrations for the competition
            var regs = registrationsResult.Value.Item2.Where(i => i.CompetitionId == competitionId && i.Status == RegistrationStatus.Accepted).ToList();

            // Get all participants for the competition, by looking at the registrations
            var parts = regs.Select(i => i.Participant);

            // Get all scores for the participants, by looking at previous matches round

            // Get all rounds for the competition
            var roundsResult = await _roundRepository.GetRoundIdsByCompetitionId(competitionId, null, null);

            if (!roundsResult.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(roundsResult.Error!);
            }

            // Select the roundId given the roundSequenceNumber
            var roundId = roundsResult.Value.Item2
                .Where(round => round.SequenceNumber == roundSequenceNumber)
                .Select(round => round.Id)
                .FirstOrDefault();

            // Get the matches from the previous round
            var prevRoundMatches = await _matchService.GetAllAsync(null, null);

            if (!prevRoundMatches.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(prevRoundMatches.Error!);
            }

            // Get the match ids from the previous round
            var prevRoundMatchIds = prevRoundMatches.Value.Item2
                .Where(match => match.RoundId == roundId)
                .Select(i => i.Id).ToList();

            var scoresResult = await _scoreService.GetAllAsync(null, null);

            if (!scoresResult.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(scoresResult.Error!);
            }


            // Filter scores where MatchId is in prevRoundMatchIds
            var scoresFromPreviousMatches = scoresResult.Value.Item2
                .Where(score => prevRoundMatchIds.Contains((Guid)score.MatchId)) // Check if MatchId is in the list
                .Select(score => score) // Extract the Score property
                .ToList(); // Convert the result to a list



            //var participantScores = scoresResult.Value.Item2
            //.Where(i => parts != null && parts.Any(p => p.Id == i.ParticipantId))
            //.ToList();


            //var timeFaultScores = participantScores
            //.Where(score => score is TimeFaultScoreDTO)
            //.Cast<TimeFaultScoreDTO>()
            //.ToList();


            var timeFaultScores = scoresFromPreviousMatches
            .Where(score => score is TimeFaultScoreDTO)
            .Cast<TimeFaultScoreDTO>()
            .ToList();


            var relevantParticipantIds = timeFaultScores
                .Where(score => timeFaultCriteria((score.Faults, score.Time)))
                .Select(score => score.ParticipantId)
                .ToList();

            var matches = new List<Match>(); // FIX



            // I need to create some matches (Type unknown) and return matchDTOs
            // Map matches to MatchDTO

            var matchDTOS = matches.Select(m => _mapper.Map<MatchDTO>(m)).ToList();

            // See if i can optimize by making specific queries to the database instead of getting all objects...

            return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Success(new Tuple<int, IEnumerable<MatchDTO>>(matchDTOS.Count, matchDTOS));


            //var filters = new Dictionary<string, object>();
            //if (roundSequenceNo > 0)
            //{
            //    filters = new Dictionary<string, object>
            //    {
            //        { "CompetitionId", competitionId },
            //        { "RoundSequenceNr", roundSequenceNo-1 }
            //        //{ "Status", RegistrationStatus.Accepted}
            //    };
            //}
            //else
            //{
            //    filters = new Dictionary<string, object>
            //    {
            //        { "CompetitionId", competitionId }
            //        //{ "Status", RegistrationStatus.Accepted}
            //    };
            //}

            //if (filters.Count == 0)
            //    return BadRequest("Invalid round sequence number");

            //var result = await _registrationService.SearchAllAsync(null, null, filters);


            //if (result.IsSuccess)
            //{
            //    return Ok(result.Value);
            //}
            //return BadRequest(result.Error);


            // Test case:
            // Comp1 -> round1 -> {match1: ekvipage1 -> score7, match2: ekvipage2 -> score8}

        }

    }
}
