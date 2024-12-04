using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : GenericsController<RoundDTO>, IRoundAPI
    {
        private IRoundService _roundService;
        public RoundsController(IGenericService<RoundDTO> service, IRoundService roundService) : base(service)
        {
            _roundService = roundService;
        }


        [HttpGet("competition/{competitionId}")]
        public async Task<ActionResult> GetRoundIdsByCompetitionIdAsync(Guid competitionId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber)
        {
            var result = await _roundService.GetByCompetitionId(competitionId, pageSize, pageNumber);
            if (result.IsSuccess && result.Value != null)
            {
                var obj = new PaginationWrapperDTO<IEnumerable<RoundDTO>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj); // Return NoContent for successful deletion
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }


        [HttpPost("CreateMatchesForRound")]
        public async Task<IActionResult> CreateMatchesForRoundAsync(Guid competitionId, uint roundSequenceNo, CriteriaDTO? criteria, [FromQuery] int? pageSize, [FromQuery] int? pageNumber)
        {

            var result = await _roundService.CreateMatchesForRoundAsync(competitionId, roundSequenceNo, criteria, null, null);
            if (result.IsSuccess && result.Value != null)
            {
                // TODO: Should we use paginationWrapper here?
                var obj = new PaginationWrapperDTO<IEnumerable<MatchDTO>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj); // Return NoContent for successful deletion
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }
    }
}
