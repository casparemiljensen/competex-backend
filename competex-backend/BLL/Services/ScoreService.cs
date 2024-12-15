using AutoMapper;
using Common.ResultPattern;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.BLL.Services
{
    public class ScoreService : GenericService<Score, ScoreDTO>, IScoreService
    {
        private readonly IMapper _mapper;
        private readonly IScoreRepository _scoreRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPenaltyRepository _penaltyRepository;

        public ScoreService(IGenericRepository<Score> repository, IMapper mapper, IRoundRepository roundRepository, IMatchRepository matchRepository, IPenaltyRepository penaltyRepository)
    : base(repository, mapper)
        {
            _scoreRepository = (IScoreRepository)repository;
            _mapper = mapper;
            _roundRepository = roundRepository;
            _matchRepository = matchRepository;
            _penaltyRepository = penaltyRepository;
        }

        public async Task<ResultT<Tuple<int, IEnumerable<ScoreDTO>>>> GetScoresForParticipant(Guid participantId, int? pageSize, int? pageNumber)
        {
            var result = await _scoreRepository.GetAllAsync(null, null);

            if (!result.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<ScoreDTO>>>.Failure(result.Error!);
            }

            //var entities = result.Value.Item2.Select(m => _mapper.Map<TDto>(m));

            var scores = result.Value.Item2.Where(i => i.ParticipantId == participantId);
            //var result = scores.Item2.Where(i => i.ParticipantId == participantId);


            var scoreDTOS = scores.Select(m => _mapper.Map<ScoreDTO>(m)).ToList();

            return ResultT<Tuple<int, IEnumerable<ScoreDTO>>>.Success(new Tuple<int, IEnumerable<ScoreDTO>>(result.Value.Item1, scoreDTOS));

            //return ResultT<Tuple<int, IEnumerable<TDto>>>.Success(new Tuple<int, IEnumerable<TDto>>(result.Value.Item1, entities));

        }

        public async Task<ResultT<PaginationWrapperDTO<IEnumerable<ScoreResultDTO>>>> GetResultsByCompetitionId(Guid competitionId, int? pageSize, int? pageNumber)
        {
            var matchFilter = new Dictionary<string, object>()
            {
                { "competitionId", competitionId }
            };
            var roundsIds = (await SearchHelper.GetAllSearch<Round, IRoundRepository>(_roundRepository, matchFilter)).Select(x => x.Id);

            var participantIds = new List<Guid>();

            var roundFilter = new Dictionary<string, object>()
            {
                { "roundId", roundsIds }
            };
            var test = (await SearchHelper.GetAllSearch<Match, IMatchRepository>(_matchRepository, roundFilter));
            participantIds.AddRange((await SearchHelper.GetAllSearch<Match, IMatchRepository>(_matchRepository, roundFilter))
                .SelectMany(x => x.ParticipantIds ?? []));

            var scoreFilter = new Dictionary<string, object>()
            {
                { "participantId", participantIds }
            };

            var scoreGroups = (await SearchHelper.GetAllSearch<Score, IScoreRepository>(_scoreRepository, scoreFilter)).GroupBy(x => x.ParticipantId);
            var emptyResult = new ScoreResult
            {
                Time = new TimeSpan(),
                Faults = 0,
                ParticipantId = Guid.Empty,
                CompetitionId = Guid.Empty
            };

            var result = scoreGroups.Select(x =>
            {
                return x.Aggregate(emptyResult, (acc, item) =>
                {
                    if (item is TimeFaultScore score)
                    {
                        return new ScoreResult
                        {
                            CompetitionId = competitionId,
                            ParticipantId = score.ParticipantId,
                            Faults = acc.Faults + score.Faults,
                            Time = acc.Time + score.Time
                        };
                    }
                    return acc;
                });
            }).ToList();
            var numberOfLines = result.Count();

            var dtoResult = result.Select(x => _mapper.Map<ScoreResultDTO>(x));

            var sortedResultDTO = dtoResult.ToList(); //Does not mutate
            sortedResultDTO.Sort(); //Mutates

            return ResultT<PaginationWrapperDTO<IEnumerable<ScoreResultDTO>>>.Success(new PaginationWrapperDTO<IEnumerable<ScoreResultDTO>>(
                sortedResultDTO,
                pageSize ?? Defaults.PageSize,
                pageNumber ?? Defaults.PageNumber,
                PaginationHelper.GetTotalPages(pageSize, pageNumber, numberOfLines)
                ));
        }



        public async Task<ResultT<IActionResult>> AddPenaltyById(Guid ScoreId, Guid PenaltyId)
        {
            var scoreTask = _scoreRepository.GetByIdAsync(ScoreId);
            var penaltyTask = _penaltyRepository.GetByIdAsync(PenaltyId);

            await Task.WhenAll(scoreTask, penaltyTask);
            if (!scoreTask.Result.IsSuccess)
            {
                return ResultT<IActionResult>.Failure(Error.NotFound("404", "No score with that ID"));
            }
            if (!penaltyTask.Result.IsSuccess)
            {
                return ResultT<IActionResult>.Failure(Error.NotFound("404", "No penalty with that ID"));
            }

            scoreTask.Result.Value.PenaltyIds.Add(ScoreId);

            var result = await _scoreRepository.UpdateAsync(scoreTask.Result.Value.Id, scoreTask.Result.Value);
            return !result.IsSuccess ? new OkResult() : ResultT<IActionResult>.Failure(result.Error!);
        }
    }
}
