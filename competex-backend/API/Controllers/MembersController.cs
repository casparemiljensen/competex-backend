using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
