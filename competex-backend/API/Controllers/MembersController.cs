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

        // When merged with Ilums changes.
        // change to async Task<IActionResult> GetById(Guid id)
        // Add await

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var obj = _memberService.GetById(id);

            if(obj != null)
            {
                return Ok(obj);
            }
            return BadRequest("An error occured");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var obj = _memberService.GetAll().Select(m => $"{m.FirstName} {m.LastName}");
            if(obj != null)
            {
                return Ok(obj);
            }
            return BadRequest("An error occured");
        }

        [HttpPost]
        public IActionResult Create(MemberDTO obj)
        {
            var res = _memberService.Create(obj);
            if (res)
            {
                return Ok();
            }
            return BadRequest("An error occured");
        }

        [HttpPut("{id}")]
        public IActionResult Update(MemberDTO obj)
        {
            var res = _memberService.Update(obj);
            if (res)
            {
                return Ok();
            }
            return BadRequest("An error occured");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _memberService.Remove(id);
            if (res)
            {
                return Ok();
            }
            return BadRequest("An error occured");
        }
    }
}
