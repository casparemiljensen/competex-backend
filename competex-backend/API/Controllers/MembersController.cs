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
    public class MembersController : GenericsController<MemberDTO>, IMemberAPI
    {
        private readonly IMemberService _memberService;

        public MembersController(IGenericService<MemberDTO> service, IMemberService memberService) : base(service)
        {
            _memberService = memberService;
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
