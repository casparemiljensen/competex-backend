using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase, IRoundAPI
    {
        private IRoundService _roundService;

        public RoundsController(IRoundService roundService)
        {
            _roundService = roundService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _roundService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error); // Return NotFound with error details
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _roundService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RoundDTO obj)
        {
            var result = await _roundService.CreateAsync(obj);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Return Created response
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] RoundDTO obj)
        {
            // You may want to include the id in the obj for identification

            var result = await _roundService.UpdateAsync(id, obj);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful update
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _roundService.RemoveAsync(id);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful deletion
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpGet("competition/{competitionId}")]
        public async Task<ActionResult> GetRoundIdsByCompetitionId(Guid competitionId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber)
        {
            var result = await _roundService.GetByCompetitionId(competitionId, pageSize, pageNumber);
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value); // Return NoContent for successful deletion
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }
    }
}
