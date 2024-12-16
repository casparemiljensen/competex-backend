using competex_backend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubMembershipRepository : IGenericRepository<ClubMembership>
    {
        //Task<Result> AddMemberToClubAsync(Guid memberId, Guid clubId, ClubMemberRole role);
        Task<ResultT<Tuple<int, IEnumerable<Member>>>> GetMembersOfClubAsync(Guid clubId, int? pageSize, int? pageNumber);
        Task<ResultT<Tuple<int, IEnumerable<Club>>>> GetClubsOfMemberAsync(Guid memberId, int? pageSize, int? pageNumber);
    }
}
