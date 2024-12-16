using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class MatchesController : GenericsController<MatchDTO>, IMatchAPI
    {
        private IMatchService _matchService;
        public MatchesController(IGenericService<MatchDTO> service, IMatchService matchService) : base(service)
        {
            _matchService = matchService;
        }

        [HttpGet("round/{roundId}")]
        public async Task<IActionResult> GetMatchesByRoundIdAsync(Guid roundId, int? pageSize, int? pageNumber)
        {
            var result = await _matchService.GetMatchesByRoundIdAsync(roundId, pageSize, pageNumber);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }
    }
}