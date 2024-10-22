using competex_backend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubMemberRepository
    {
        void AddMemberToClub(Guid memberId, Guid clubId);
        public List<Member> GetMembersOfClub(Guid clubId);
        public List<Club> GetClubsOfMember(Guid memberId);
    }
}
