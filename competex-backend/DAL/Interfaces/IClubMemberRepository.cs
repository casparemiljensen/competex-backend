using competex_backend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubMemberRepository
    {
        Task AddMemberToClubAsync(Guid memberId, Guid clubId, ClubMemberRole role);
        Task UpdateClubMemberAsync(ClubMember clubmember);
        Task DeleteMemberFromClubAsync(Guid clubId, Guid memberId); // Could also use ClubMemberId if available when removing a member from a club

        Task<ClubMember?> GetClubMemberFromIdAsync(Guid clubMemberId);
        Task<List<Member>> GetMembersOfClubAsync(Guid clubId);
        Task<List<Club>> GetClubsOfMemberAsync(Guid memberId);
        Task CreateEventAsync();
    }
}
