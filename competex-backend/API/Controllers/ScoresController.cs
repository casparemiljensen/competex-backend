using Common.ResultPattern;
using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Common.Helpers;
using competex_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : GenericsController<ScoreDTO>, IScoreAPI
    {
        private IScoreService _scoreService;
        private IMatchService _matchService;
        private IRoundService _roundService;
        public ScoresController(IGenericService<ScoreDTO> service, IScoreService scoreService, IMatchService matchService, ICompetitionService competitionService, IRoundService roundService) : base(service)
        {
            _scoreService = scoreService;
            _matchService = matchService;
            _roundService = roundService;
        }

        public async List<T> GetAllSearch<T, SType>(SType service, Dictionary<string, object> filter) where SType : IGenericService<T> where T : class
        {
            var batchSize = 10;
            var localPageNumber = 1;
            var totalPageNumber = 0;
            List<T> output = [];
            var competitionResult = await service.SearchAllAsync(batchSize, localPageNumber, filter);
            localPageNumber++;
            if (!competitionResult.IsSuccess)
            {
                return NotFound(competitionResult.Error);
            }

            output.AddRange(competitionResult.Value.Item2);

            totalPageNumber = PaginationHelper.GetTotalPages(batchSize, localPageNumber, competitionResult.Value.Item1);
            List<Task<ResultT<Tuple<int, IEnumerable<T>>>>> tasks = [];
            while (localPageNumber <= totalPageNumber)
            {
                tasks.Add(service.SearchAllAsync(batchSize, localPageNumber, filter));
                localPageNumber++;
            }

            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                foreach (var a in task.Result.Value.Item2)
                {
                    output.Add(a);
                }
            }
            return output;
        }

        [HttpGet("/getResults/{competitionId}")]
        public async Task<IActionResult> GetResultsOfMatch(Guid competitionId, int? pageSize, int? pageNumber)
        {
           
            

            if (!competitionResult.IsSuccess)
            {
                return NotFound(competitionResult.Error);
            }

            var matchIds = competitionResult.Value


            var matchResult = await _matchService.GetByIdAsync(MatchId);

            if (!matchResult.IsSuccess)
            {
                return NotFound(matchResult.Error);
            }

            var matchId = matchResult.Value.RoundId;
            var participantIds = matchResult.Value.ParticipantIds;

            List<Task> results = new List<Task>();

            

            foreach (Guid id in participantIds)
            {
                Dictionary<string, object> filter = new Dictionary<string, object>
                {
                    { "ParticipantId", id }
                };
                results.Add(_scoreService.SearchAllAsync(10, 0, filter));
            }

            await Task.WhenAll(results);
            
            var resultScore = new ScoreResult()
            {
                resu
            }

            var resultValues = results.Aggregate( => )
            if (result.IsSuccess)
            {
                var obj = new PaginationWrapperDTO<IEnumerable<ClubDTO>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj);
            }
            return NotFound(result.Error); // Return NotFound with error details if no clubs found
        }
    }
}
