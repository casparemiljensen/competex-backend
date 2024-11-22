using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubMembershipController : GenericsController<ClubMembershipDTO>, IClubMemberShipAPI
    {
        private IClubMembershipService _clubMembershipService;
        public ClubMembershipController(IGenericService<ClubMembershipDTO> service, IClubMembershipService clubMembershipService) : base(service)
        {
            _clubMembershipService = clubMembershipService;
        }

        // GET: api/ClubMembership/members/{clubId}
        [HttpGet("members/{clubId}")]
        public async Task<IActionResult> GetMembersOfClubAsync(Guid clubId, int? pageSize, int? pageNumber)
        {
            var result = await _clubMembershipService.GetMembersOfClubAsync(clubId, pageSize, pageNumber);
            if (result.IsSuccess)
            {
                var obj = new PaginationWrapperDTO<IEnumerable<MemberDTO>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj);
            }
            return NotFound(result.Error); // Return NotFound with error details if no members found
        }


        // GET: api/ClubMembership/clubs/{memberId}
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
