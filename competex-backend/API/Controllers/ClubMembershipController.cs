using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubMembershipController : ControllerBase
    {
        private IClubMembershipService _clubMembershipService;
        public ClubMembershipController(IClubMembershipService clubMembershipService)
        {
            _clubMembershipService = clubMembershipService;
        }

        // GET: api/<ClubMembershipController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _clubMembershipService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error); // Return NotFound with error details
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var result = await _clubMembershipService.GetAllAsync(pageSize, pageNumber);
            if (result.IsSuccess)
            {
                var obj = result.Value.Select(m => $"{m.MemberId} - {m.JoinDate}");
                return Ok(obj);
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ClubMembershipDTO obj)
        {
            var result = await _clubMembershipService.CreateAsync(obj);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Return Created response
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(ClubMembershipDTO obj)
        {
            // You may want to include the id in the obj for identification

            var result = await _clubMembershipService.UpdateAsync(obj);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful update
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _clubMembershipService.RemoveAsync(id);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful deletion
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        // GET: api/ClubMembership/members/{clubId}
        [HttpGet("members/{clubId}")]
        public async Task<IActionResult> GetMembersOfClubAsync(Guid clubId)
        {
            var result = await _clubMembershipService.GetMembersOfClubAsync(clubId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error); // Return NotFound with error details if no members found
        }


        // GET: api/ClubMembership/clubs/{memberId}
        [HttpGet("clubs/{memberId}")]
        public async Task<IActionResult> GetClubsOfMemberAsync(Guid memberId)
        {
            var result = await _clubMembershipService.GetClubsOfMemberAsync(memberId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error); // Return NotFound with error details if no clubs found
        }


    }
}
