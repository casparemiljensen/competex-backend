using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace competex_backend.API.Controllers
{
    [Route("api/[round]")]
    [ApiController]
    public class RoundController : ControllerBase, IRoundAPI
    {
        private IMemberService _memberService;

        public RoundController(IMemberService memberService)
        {
            _memberService = memberService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _memberService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error); // Return NotFound with error details
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _memberService.GetAllAsync();
            if (result.IsSuccess)
            {
                var obj = result.Value.Select(m => $"{m.FirstName} {m.LastName} - {m.Id}");
                return Ok(obj);
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MemberDTO obj)
        {
            var result = await _memberService.CreateAsync(obj);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Return Created response
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(MemberDTO obj)
        {
            // You may want to include the id in the obj for identification

            var result = await _memberService.UpdateAsync(obj);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful update
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _memberService.RemoveAsync(id);
            if (result.IsSuccess)
            {
                return NoContent(); // Return NoContent for successful deletion
            }
            return BadRequest(result.Error); // Return BadRequest with error details
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string firstName)
        {
            var res = _memberService.GetByName(firstName);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("An error occured");
        }

        public Task<IActionResult> CreateAsync(RoundDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateAsync(RoundDTO obj)
        {
            throw new NotImplementedException();
        }

        ActionResult IRoundAPI.GetRoundIdsByCompetitionId(Guid competitionId)
        {
            throw new NotImplementedException();
        }
    }
}
