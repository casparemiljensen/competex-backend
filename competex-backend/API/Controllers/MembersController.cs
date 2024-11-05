using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase, IMemberAPI
    {
        private IMemberService _memberService;

        public MembersController(IMemberService memberService)
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
        public async Task<IActionResult> UpdateAsync(Guid id,MemberDTO obj)
        {
            // You may want to include the id in the obj for identification

            var result = await _memberService.UpdateAsync(id, obj);
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

        [HttpGet("GetNumber")]
        public IActionResult GetNumber()
        {
            var res = _memberService.CheckNumber();
            if (res)
            {
                return Ok();
            }
            return BadRequest("An error occured");
        }
    }
}
