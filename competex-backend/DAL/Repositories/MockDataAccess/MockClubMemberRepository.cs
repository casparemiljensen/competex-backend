using AutoMapper.Execution;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;
using Member = competex_backend.Models.Member;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubMemberRepository : IClubMemberRepository
    {
        private List<ClubMember> _clubMembers;
        private readonly List<Club> _clubs;
        private readonly List<Member> _members;

        public MockClubMemberRepository(List<Club> clubs, List<Member> members)
        {
            _clubs = clubs;
            _members = members;
            _clubMembers = new List<ClubMember>();
        }


        public void AddMemberToClub(Guid memberId, Guid clubId) // Maybe it makes more sense to just take the Guids here.
        {

            var club = _clubs.FirstOrDefault(c => c.ClubId == clubId);
            var member = _members.FirstOrDefault(m => m.MemberId == memberId);

            if (club != null && member != null) //Check that member and club exists
            {
                var existingClubMember = _clubMembers.FirstOrDefault(cm => cm.ClubId == clubId && cm.MemberId == memberId);
                if (existingClubMember == null) //Only add member to club, if member does not already exist in the club
                {
                    var clubMember = new ClubMember
                    {
                        ClubId = clubId,
                        MemberId = memberId,
                        JoinDate = DateTime.UtcNow,
                    };
                    _clubMembers.Add(clubMember);
                }
                else
                {
                    throw new Exception("Club membership already exists");
                    //existingClubMember.Role = role; // Update role if already exists
                }
            }
        }

        public List<Club> GetClubsOfMember(Guid memberId)
        {
            return _clubMembers
                .Where(clubmember => clubmember.MemberId == memberId) // Filter on member Id
                .Select(clubmember => clubmember.Club).ToList(); // Select only list of "Club" attributes to return
        }

        public List<Member> GetMembersOfClub(Guid clubId)
        {
            return _clubMembers
                .Where(clubmember => clubmember.ClubId == clubId) // Filter on member Id
                .Select(clubmember => clubmember.Member).ToList(); // Select only list of "Club" attributes to return
        }
    }
}
