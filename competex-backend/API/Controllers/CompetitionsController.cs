using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionsController : GenericsController<CompetitionDTO>, ICompetitionAPI
    {
        private ICompetitionService _competitionService;

        public CompetitionsController(IGenericService<CompetitionDTO> service, ICompetitionService competitionService) : base(service)
        {
            _competitionService = competitionService;

        }

        [HttpPost]
        public async override Task<IActionResult> CreateAsync(CompetitionDTO obj)
        {
            var result = await _competitionService.CreateAsync(obj);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}
