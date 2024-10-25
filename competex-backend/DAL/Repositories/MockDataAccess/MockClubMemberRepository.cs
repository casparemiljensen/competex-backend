using AutoMapper.Execution;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Data;
using Member = competex_backend.Models.Member;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubMemberRepository : IClubMemberRepository
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IDatabaseManager _db;


        public MockClubMemberRepository(IDatabaseManager db, IClubRepository clubRepo, IMemberRepository memberRepo)
        {
            _clubRepository = clubRepo;
            _memberRepository = memberRepo;
            _db = db;
        }

        public void AddMemberToClub(Guid memberId, Guid clubId, ClubMemberRole role) // Maybe it makes more sense to just take the Guids here.
        {

            var club = _clubRepository.GetById(clubId);
            var member = _memberRepository.GetById(memberId);

            if (club != null && member != null) //Check that member and club exists
            {
                var existingClubMember = _db.ClubMembers.FirstOrDefault(cm => cm.ClubId == clubId && cm.MemberId == memberId);
                if (existingClubMember == null) //Only add member to club, if member does not already exist in the club
                {
                    var clubMember = new ClubMember
                    {
                        ClubId = clubId,
                        Club = club,
                        MemberId = memberId,
                        Member = member,
                        JoinDate = DateTime.UtcNow,
                    };
                    _db.ClubMembers.Add(clubMember);
                }
                else
                {
                    throw new Exception("Club membership already exists");
                    //existingClubMember.Role = role; // Update role if already exists
                }
            }
        }
        public void DeleteMemberFromClub(Guid memberId, Guid clubId)
        {
            var clubMemberToRemove = _db.ClubMembers
            .FirstOrDefault(cm => cm.ClubId == clubId && cm.MemberId == memberId);

            if (clubMemberToRemove != null)
            {
                _db.ClubMembers.Remove(clubMemberToRemove); // Remove the club-member relationship
            }
            else
            {
                throw new Exception("Club membership does not exist"); // Optional: Handle this case as needed
            }
        }

        public List<Club> GetClubsOfMember(Guid memberId)
        {
            return _db.ClubMembers
                .Where(clubmember => clubmember.MemberId == memberId) // Filter on member Id
                .Select(clubmember => clubmember.Club).ToList(); // Select only list of "Club" attributes to return
        }

        public List<Member> GetMembersOfClub(Guid clubId)
        {
            return _db.ClubMembers
                .Where(clubmember => clubmember.ClubId == clubId) // Filter on member Id
                .Select(clubmember => clubmember.Member).ToList(); // Select only list of "Club" attributes to return
        }
        public void UpdateClubMember(ClubMember clubMember)
        {
            // Find the existing club member in the list
            var existingClubMember = _db.ClubMembers.FirstOrDefault(cm => cm.ClubId == clubMember.ClubId && cm.MemberId == clubMember.MemberId);

            if (existingClubMember != null)
            {
                // Update properties as needed
                existingClubMember.JoinDate = clubMember.JoinDate; // Update JoinDate or any other properties that might be relevant
                existingClubMember.Role = clubMember.Role.GetValueOrDefault(ClubMemberRole.standard); // Set role - standard if no role is set beforehand
            }
            else
            {
                // Throw an exception if the club member does not exist
                throw new Exception("Club member not found");
            }
        }
        public ClubMember? GetClubMemberFromId(Guid clubMemberId)
        {
            return _db.ClubMembers.FirstOrDefault(cm => cm.ClubMemberId == clubMemberId); //may return null
        }



        public void CreateEvent()
        {
            throw new NotImplementedException();
        }

    }

}
