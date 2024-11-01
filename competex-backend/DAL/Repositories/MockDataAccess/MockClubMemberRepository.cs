//using AutoMapper.Execution;
//using competex_backend.DAL.Interfaces;
//using competex_backend.Models;
//using System.Data;
//using Member = competex_backend.Models.Member;

//namespace competex_backend.DAL.Repositories.MockDataAccess
//{
//    public class MockClubMemberRepository : IClubMemberRepository
//    {
//        private readonly IClubRepository _clubRepository;
//        private readonly IMemberRepository _memberRepository;
//        private readonly IDatabaseManager _db;


//        public MockClubMemberRepository(IDatabaseManager db, IClubRepository clubRepo, IMemberRepository memberRepo)
//        {
//            _clubRepository = clubRepo;
//            _memberRepository = memberRepo;
//            _db = db;
//        }


//        public async Task AddMemberToClubAsync(Guid memberId, Guid clubId, ClubMemberRole role)
//        {
//            var club = await _clubRepository.GetByIdAsync(clubId);
//            var member = await _memberRepository.GetByIdAsync(memberId);

//            if (club != null && member != null) // Check that member and club exist
//            {
//                var existingClubMember = _db.ClubMembers.FirstOrDefault(cm => cm.ClubId == clubId && cm.MemberId == memberId);
//                if (existingClubMember == null) // Only add member to club if not already in club
//                {
//                    var clubMember = new ClubMember
//                    {
//                        ClubId = clubId,
//                        Club = club,
//                        MemberId = memberId,
//                        Member = member,
//                        JoinDate = DateTime.UtcNow,
//                        Role = role
//                    };
//                    _db.ClubMembers.Add(clubMember);
//                }
//                else
//                {
//                    throw new Exception("Club membership already exists");
//                }
//            }
//        }

//        // Remove a member from a club asynchronously
//        public async Task DeleteMemberFromClubAsync(Guid memberId, Guid clubId)
//        {
//            var clubMemberToRemove = _db.ClubMembers
//                .FirstOrDefault(cm => cm.ClubId == clubId && cm.MemberId == memberId);

//            if (clubMemberToRemove != null)
//            {
//                _db.ClubMembers.Remove(clubMemberToRemove); // Remove the club-member relationship
//                await Task.CompletedTask;
//            }
//            else
//            {
//                throw new Exception("Club membership does not exist"); // Optional: Handle this case as needed
//            }
//        }

//        // Get all clubs of a member asynchronously
//        public async Task<List<Club>> GetClubsOfMemberAsync(Guid memberId)
//        {
//            var clubs = _db.ClubMembers
//                .Where(clubMember => clubMember.MemberId == memberId)
//                .Select(clubMember => clubMember.Club)
//                .ToList();

//            return await Task.FromResult(clubs);
//        }

//        // Get all members of a club asynchronously
//        public async Task<List<Member>> GetMembersOfClubAsync(Guid clubId)
//        {
//            var members = _db.ClubMembers
//                .Where(clubMember => clubMember.ClubId == clubId)
//                .Select(clubMember => clubMember.Member)
//                .ToList();

//            return await Task.FromResult(members);
//        }

//        // Update a club member asynchronously
//        public async Task UpdateClubMemberAsync(ClubMember clubMember)
//        {
//            var existingClubMember = _db.ClubMembers
//                .FirstOrDefault(cm => cm.ClubId == clubMember.ClubId && cm.MemberId == clubMember.MemberId);

//            if (existingClubMember != null)
//            {
//                // Update properties as needed
//                existingClubMember.JoinDate = clubMember.JoinDate;
//                existingClubMember.Role = clubMember.Role.GetValueOrDefault(ClubMemberRole.Standard);
//                await Task.CompletedTask;
//            }
//            else
//            {
//                throw new Exception("Club member not found");
//            }
//        }

//        // Get a club member by ID asynchronously
//        public async Task<ClubMember?> GetClubMemberFromIdAsync(Guid clubMemberId)
//        {
//            var clubMember = _db.ClubMembers.FirstOrDefault(cm => cm.ClubMemberId == clubMemberId);
//            return await Task.FromResult(clubMember);
//        }

//        public Task CreateEventAsync()
//        {
//            throw new NotImplementedException();
//        }

//    }
    
//}