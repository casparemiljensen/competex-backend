using competex_backend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubMembershipRepository : IGenericRepository<ClubMembership>
    {
        //Task<Result> AddMemberToClubAsync(Guid memberId, Guid clubId, ClubMemberRole role);
        Task<ResultT<List<Member>>> GetMembersOfClubAsync(Guid clubId);
        Task<ResultT<List<Club>>> GetClubsOfMemberAsync(Guid memberId);
        Task<Result> CreateEventAsync();
    }
}
