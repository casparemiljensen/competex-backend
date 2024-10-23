using competex_backend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubMemberRepository
    {
        public void AddMemberToClub(Guid memberId, Guid clubId, ClubMemberRole role);
        public void DeleteMemberFromClub(Guid memberId, Guid clubId);
        public List<Member> GetMembersOfClub(Guid clubId);
        public List<Club> GetClubsOfMember(Guid memberId);
    }
}
