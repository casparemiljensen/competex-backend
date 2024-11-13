using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Common.Helpers;
using competex_backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<ActionResult> GetRoundIdsByCompetitionId(Guid competitionId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber)
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
    }
}
