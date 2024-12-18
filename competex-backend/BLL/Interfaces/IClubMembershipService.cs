using competex_backend.API.DTOs;
using competex_backend.Models;

namespace competex_backend.BLL.Interfaces
{
    public interface IClubMembershipService : IGenericService<ClubMembershipDTO>
    {
        Task<ResultT<Tuple<int, IEnumerable<MemberDTO>>>> GetMembersOfClubAsync(Guid clubId, int? pageSize, int? pageNumber);
        Task<ResultT<Tuple<int, IEnumerable<ClubDTO>>>> GetClubsOfMemberAsync(Guid memberId, int? pageSize, int? pageNumber);
    }
}
