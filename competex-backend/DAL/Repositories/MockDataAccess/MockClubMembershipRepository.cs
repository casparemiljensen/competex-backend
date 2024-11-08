using AutoMapper.Execution;
using competex_backend.DAL.Filters;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Data;
using Member = competex_backend.Models.Member;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubMembershipRepository : MockGenericRepository<ClubMembership>, IClubMembershipRepository
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMemberRepository _memberRepository;


        public MockClubMembershipRepository(MockDatabaseManager db, IClubRepository clubRepo, IMemberRepository memberRepo) : base(db)
        {
            _clubRepository = clubRepo;
            _memberRepository = memberRepo;
        }



        // Get all clubs of a member asynchronously
        public async Task<ResultT<List<Club>>> GetClubsOfMemberAsync(Guid memberId)
        {
            // Get the list of Club IDs associated with the given memberId
            var clubIds = _entities
                .Where(clubMember => clubMember.MemberId == memberId)
                .Select(clubMember => clubMember.ClubId)
                .ToList();
            // Create a filter with the retrieved club IDs
            var filter = new BaseFilter { Ids = clubIds };

            // Use GetByFilterAsync to get the list of clubs based on the filter
            var clubsResult = await _clubRepository.GetAllAsync(filter);

            // Check if the operation was successful, and handle accordingly
            if (clubsResult.IsSuccess)
            {
                // Return the clubs as a List
                return ResultT<List<Club>>.Success(clubsResult.Value.ToList());
            }

            // If there was an error, return a failure result with the error details
            return ResultT<List<Club>>.Failure(clubsResult.Error ?? Error.NotFound("NoClubsFound", $"No clubs found for the member with ID {memberId}"));
        }

        // Get all members of a club asynchronously
        public async Task<ResultT<List<Member>>> GetMembersOfClubAsync(Guid clubId)
        {
            // Get the list of Member IDs associated with the given clubId
            var memberIds = _entities
                .Where(clubMember => clubMember.ClubId == clubId)
                .Select(clubMember => clubMember.MemberId)
                .ToList();

            // Create a filter with the retrieved member IDs
            var filter = new BaseFilter { Ids = memberIds };

            // Use GetByFilterAsync to get the list of members based on the filter
            var membersResult = await _memberRepository.GetAllAsync(filter);

            // Check if the operation was successful, and handle accordingly
            if (membersResult.IsSuccess && membersResult.Value is not null)
            {
                // Return the members as a List
                return ResultT<List<Member>>.Success(membersResult.Value.ToList());
            }

            // If there was an error or the result was null, return a failure result with an error message
            return ResultT<List<Member>>.Failure(membersResult.Error ?? Error.NotFound("NoMembersFound", $"No members found for the club with ID {clubId}."));
        }



        public Task<Result> CreateEventAsync()
        {
            throw new NotImplementedException();
        }

    }

}