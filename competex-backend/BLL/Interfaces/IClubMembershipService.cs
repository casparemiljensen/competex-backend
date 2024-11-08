using competex_backend.API.DTOs;
using competex_backend.Models;

namespace competex_backend.BLL.Interfaces
{
    public interface IClubMembershipService : IGenericService<ClubMembershipDTO>
    {
        Task<ResultT<List<Member>>> GetMembersOfClubAsync(Guid clubId);
        Task<ResultT<List<Club>>> GetClubsOfMemberAsync(Guid memberId);
        Task<Result> CreateEventAsync();
    }
}
