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
        public async Task<IActionResult> GetById(Guid id)
        {
            var obj = await _memberService.GetByIdAsync(id);

            if(obj != null)
            {
                return Ok(obj);
            }
            return BadRequest("An error occured");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var obj = await _memberService.GetAllAsync();
            var res = obj.Select(m => $"{m.FirstName} {m.LastName}");
            if(res != null)
            {
                return Ok(res);
            }
            return BadRequest("An error occured");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberDTO obj)
        {
            var res = await _memberService.CreateAsync(obj);
            if (res)
            {
                return Ok();
            }
            return BadRequest("An error occured");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(MemberDTO obj)
        {
            // TODO: Something is wrong here
            var res = await _memberService.UpdateAsync(obj);
            if (res)
            {
                return Ok();
            }
            return BadRequest("An error occured");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _memberService.RemoveAsync(id);
            if (res)
            {
                return Ok();
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
    }
}
