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

        public List<T> GetAllSearch<T, RType>()
        {

        }

        [HttpGet("/getResults/{competitionId}")]
        public async Task<IActionResult> GetResultsOfMatch(Guid competitionId, int? pageSize, int? pageNumber)
        {
            var batchSize = 10;
            var localPageNumber = 0;
            var totalPageNumber = 0;
            var competitionResult = await _roundService.SearchAllAsync(batchSize, localPageNumber, new Dictionary<string, object>()
                {
                    { "CompetitionId", competitionId }
                });
            if (!competitionResult.IsSuccess)
            {
                return NotFound(competitionResult.Error);
            }
            totalPageNumber = PaginationHelper.GetTotalPages(batchSize, localPageNumber, competitionResult.Value.Item1);
            while (localPageNumber < totalPageNumber) {
                
                localPageNumber++;
            }

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
                com
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
