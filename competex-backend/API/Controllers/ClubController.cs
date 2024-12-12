using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : GenericsController<ClubDTO>, IClubAPI
    {
        private IClubMembershipService _clubMembershipService;
        public ClubController(IGenericService<ClubDTO> service, IClubMembershipService clubMembershipService) : base(service)
        {
            _clubMembershipService = clubMembershipService;
        }

        // GET: api/club/{clubId}/members
        [HttpGet("{clubId}/members")]
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
    }
}
