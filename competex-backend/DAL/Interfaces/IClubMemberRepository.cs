using competex_backend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubMemberRepository
    {
        public void AddMemberToClub(Guid memberId, Guid clubId, ClubMemberRole role);
        public void UpdateClubMember(ClubMember clubmember);
        public void DeleteMemberFromClub(Guid clubId, Guid memberId); // Kunne også have lavet denne metode med ClubMemberId i stedet. Det kommer han på om vi har det Id når vi gerne vil slette en person fra en klub

        public ClubMember GetClubMemberFromId(Guid clubMemberId);
        public List<Member> GetMembersOfClub(Guid clubId);
        public List<Club> GetClubsOfMember(Guid memberId);
        public void CreateEvent();
    }
}
