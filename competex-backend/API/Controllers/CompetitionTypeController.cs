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
    public class CompetitionTypeController : ControllerBase, ICompetitionTypeAPI
    {
        private ICompetitionTypeService _competitionTypeService;

        public CompetitionTypeController(ICompetitionTypeService competitionTypeService)
        {
            _competitionTypeService = competitionTypeService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _competitionTypeService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error); // Return NotFound with error details
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var result = await _competitionTypeService.GetAllAsync(pageSize, pageNumber);
            if (result.IsSuccess)
            {
                var obj = new PaginationWrapperDTO<IEnumerable<CompetitionTypeDTO>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj);
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompetitionTypeDTO obj)
        {
            var result = await _competitionTypeService.CreateAsync(obj);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Return Created response
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CompetitionTypeDTO obj)
        {
            // You may want to include the id in the obj for identification

            var result = await _competitionTypeService.UpdateAsync(id, obj);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful update
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _competitionTypeService.RemoveAsync(id);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful deletion
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }
    }
}
