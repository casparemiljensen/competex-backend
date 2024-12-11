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
        /// <summary>
        /// Gets all results for a competition. Tip: Use search on participant domain, to get a lot of participants
        /// </summary>
        /// <param name="CompetitionId">The ID of the competition which results will be gotten calculated.</param>
        /// <returns>An IActionResult indicating the operation result.</returns>
        [HttpGet("/getResults/{CompetitionId}")]
        public async Task<IActionResult> GetResultsByCompetitionId(Guid CompetitionId, int? pageSize, int? pageNumber)
        {
            var result = await _scoreService.GetResultsByCompetitionId(CompetitionId, pageSize, pageNumber);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error!);
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// Adds a penalty to the specified score.
        /// </summary>
        /// <param name="ScoreId">The ID of the score to which the penalty will be added.</param>
        /// <param name="PenaltyId">The ID of the penalty to be added.</param>
        /// <returns>An IActionResult indicating the operation result.</returns>
        [HttpPost("{ScoreId}/addPenaltyById/{PenaltyId}")]
        public async Task<IActionResult> AddPenaltyById(Guid ScoreId, Guid PenaltyId)
        {
            return Ok(await _scoreService.AddPenaltyById(ScoreId ,PenaltyId));
        }
    }
}
