using AutoMapper;
using Common.ResultPattern;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace competex_backend.BLL.Services
{
    public class RoundService : GenericService<Round, RoundDTO>, IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IMapper _mapper;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IMatchRepository _matchRepository;

        public RoundService(IGenericRepository<Round> repository, IMapper mapper, IRegistrationRepository registrationRepository, IScoreRepository scoreRepository, IMatchRepository matchRepository) : base(repository, mapper)
        {
            _roundRepository = (IRoundRepository)repository;
            _mapper = mapper;
            _registrationRepository = registrationRepository;
            _scoreRepository = scoreRepository;
            _matchRepository = matchRepository;
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


        public async Task<ResultT<Tuple<int, IEnumerable<MatchDTO>>>> CreateMatchesForRoundAsync(Guid competitionId, uint roundSequenceNumber, CriteriaDTO? criteria, int? pageSize, int? pageNumber)
        {

            // We might set match state here
            // See if i can optimize by making specific queries to the database instead of getting all objects...
            // Right now the round has to be made beforehand
            var regFilter = new Dictionary<string, object>()
            {
                { "CompetitionId", competitionId },
                { "Status", RegistrationStatus.Accepted }
            };

            var regs = await SearchHelper.GetAllSearch<Registration, IRegistrationRepository>(_registrationRepository, regFilter);


            var roundFilter = new Dictionary<string, object>()
            {
                { "CompetitionId", competitionId },
                { "SequenceNumber", (int)roundSequenceNumber }
            };
            // Get all rounds for the competition
            var roundsResult = await SearchHelper.GetAllSearch<Round, IRoundRepository>(_roundRepository, roundFilter);

            var roundTwoFilter = new Dictionary<string, object>()
            {
                { "CompetitionId", competitionId },
                { "SequenceNumber", (int)roundSequenceNumber }
            };
            // Get all rounds for the competition
            var roundTwoResult = await SearchHelper.GetAllSearch<Round, IRoundRepository>(_roundRepository, roundTwoFilter);

            var idOfNewRound = roundsResult
            .Select(round => round.Id)
            .FirstOrDefault();


            if (idOfNewRound == Guid.Empty)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(Error.NotFound("404", "Current round not found"));
            }


            var relevantParticipantIds = new List<Guid>();

            if (criteria != null)
            {
                Func<(int Fault, TimeSpan Time), bool> timeFaultCriteria = tuple =>
                    tuple.Fault <= criteria.MaxFaults && tuple.Time <= criteria.MaxMinutes;

                var idOfPrevRound = Guid.Empty;

                // Select the roundId given the roundSequenceNumber
                if (roundSequenceNumber > 0) // Epic fail? > 1
                {
                    idOfPrevRound = roundTwoResult
                    .Select(round => round.Id)
                    .FirstOrDefault();
                }
                else
                {
                    idOfPrevRound = roundTwoResult
                    .Select(round => round.Id)
                    .FirstOrDefault();
                }

                if (idOfPrevRound == Guid.Empty)
                {
                    return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(Error.NotFound("404", "Previous round not found"));
                }


                // Get the matches from the previous round

                var matchFilter = new Dictionary<string, object>()
                {
                    { "RoundId", idOfPrevRound },
                };
                var prevRoundMatches = await SearchHelper.GetAllSearch<Match, IMatchRepository>(_matchRepository, matchFilter);

                var prevRoundMatchIds = prevRoundMatches.Select(match => match.Id).ToList();

                var scoreFilter = new Dictionary<string, object>()
                {
                    { "ScoreType", "TimeFault" },
                    { "MatchId", prevRoundMatchIds }
                };
                var scoresFromPreviousMatches = await SearchHelper.GetAllSearch<Score, IScoreRepository>(_scoreRepository, scoreFilter);
                var timeFaultScores = scoresFromPreviousMatches.Cast<TimeFaultScore>().ToList();
                //var scoresResult = await _scoreRepository.GetAllAsync(null, null);

                //if (!scoresResult.IsSuccess)
                //{
                //    return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(scoresResult.Error!);
                //}

                // Select match ids from the previous round

                // Filter scores where MatchId is in prevRoundMatchIds
                //var scoresFromPreviousMatches = scoresResult.Value.Item2
                //    .Where(score => prevRoundMatchIds.Contains((Guid)score.MatchId)) // Check if MatchId is in the list
                //    .Select(score => score) // Extract the Score property
                //    .ToList(); // Convert the result to a list


                // Select all relevant types of scores, here timeFaultScores are selected - since they are used in rabbit jumping.
                //var timeFaultScores = scoresFromPreviousMatches
                //.Where(score => score is TimeFaultScore)
                //.Cast<TimeFaultScore>()
                //.ToList();

                // Select all relevant participants, based on the criteria
                relevantParticipantIds = timeFaultScores
                    .Where(score => timeFaultCriteria((score.Faults, score.Time)))
                    .Select(score => score.ParticipantId)
                    .ToList();
            }
            else
            {
                relevantParticipantIds = regs.Select(reg => reg.ParticipantId).ToList();
            }


            if (relevantParticipantIds.Count == 0)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(Error.NotFound("404", "No relevant participants found"));
            }

            int noOfMatchesCreated = 0;
            foreach (var participantId in relevantParticipantIds)
            {
                Match match = new Match
                {
                    Id = Guid.NewGuid(), // Db should handle this
                    RoundId = idOfNewRound,
                    ParticipantIds = new List<Guid> { participantId },
                    Status = MatchStatus.Pending
                };
                await _matchRepository.InsertAsync(match);
                noOfMatchesCreated++;
            }

            // Ideally we would return ids of inserted matches instead of fetching.

            if (noOfMatchesCreated == 0)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(Error.Failure("500", "No new matches created"));
            }

            var matchesResult = await _matchRepository.GetMatchesByRoundId(idOfNewRound, pageSize, pageNumber);

            if (!matchesResult.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(matchesResult.Error!);
            }

            var matchDTOs = matchesResult.Value.Item2.Select(m => _mapper.Map<MatchDTO>(m)).ToList();

            return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Success(new Tuple<int, IEnumerable<MatchDTO>>(matchDTOs.Count, matchDTOs));
        }
    }
}
