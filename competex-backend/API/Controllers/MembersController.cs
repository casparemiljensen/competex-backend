using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : GenericsController<MemberDTO>, IMemberAPI
    {
        private readonly IMemberService _memberService;
        private IClubMembershipService _clubMembershipService;

        public MembersController(IGenericService<MemberDTO> service, IMemberService memberService, IClubMembershipService clubMembershipService) : base(service)
        {
            _memberService = memberService;
            _clubMembershipService = clubMembershipService;
        }

        // GET: api/member/clubs/{memberId}
        [HttpGet("clubs/{memberId}")]
        public async Task<IActionResult> GetClubsOfMemberAsync(Guid memberId, int? pageSize, int? pageNumber)
        {
            var result = await _clubMembershipService.GetClubsOfMemberAsync(memberId, pageSize, pageNumber);
            if (result.IsSuccess)
            {
                var obj = new PaginationWrapperDTO<IEnumerable<ClubDTO>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj);
            }
            return NotFound(result.Error); // Return NotFound with error details if no clubs found
        }
    }
}
