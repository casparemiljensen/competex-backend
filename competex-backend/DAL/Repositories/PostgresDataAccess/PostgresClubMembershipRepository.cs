using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresClubMembershipRepository : PostgresGenericRepository<ClubMembership>, IClubMembershipRepository
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMemberRepository _memberRepository;


        public PostgresClubMembershipRepository(ApplicationDbContext dbContext, IClubRepository clubRepo, IMemberRepository memberRepo)
            : base(dbContext)
        {
            _clubRepository = clubRepo;
            _memberRepository = memberRepo;
        }

        // Get all clubs of a member asynchronously
        public async Task<ResultT<Tuple<int, IEnumerable<Club>>>> GetClubsOfMemberAsync(Guid memberId, int? pageSize, int? pageNumber)
        {
            // Get the list of Club IDs associated with the given memberId
            var clubIds = _dbContext.ClubMemberships // TODO: Check this still works, after rewriting.
                .Where(clubMember => clubMember.MemberId == memberId) // Single-line lambda
                .Select(clubMember => clubMember.ClubId)
                .ToList();
            // Create a filter with the retrieved club IDs
            var filter = new Dictionary<string, object>() { { "Id", clubIds } }; //TODO: FIX

            // Use GetByFilterAsync to get the list of clubs based on the filter
            var clubsResult = await _clubRepository.SearchAllAsync(pageSize, pageNumber, filter);

            // Check if the operation was successful, and handle accordingly
            if (clubsResult.IsSuccess)
            {
                // Return the clubs as a List
                return clubsResult;
            }

            // If there was an error, return a failure result with the error details
            return ResultT<Tuple<int, IEnumerable<Club>>>.Failure(clubsResult.Error ?? Error.NotFound("NoClubsFound", $"No clubs found for the member with ID {memberId}"));
        }

        // Get all members of a club asynchronously
        public async Task<ResultT<Tuple<int, IEnumerable<Member>>>> GetMembersOfClubAsync(Guid clubId, int? pageSize, int? pageNumber)
        {
            // Get the list of Member IDs associated with the given clubId
            var memberIds = _dbContext.ClubMemberships
                .Where(clubMember => clubMember.ClubId == clubId)
                .Select(clubMember => clubMember.MemberId)
                .ToList();

            // Create a filter with the retrieved member IDs
            var filter = new Dictionary<string, object>() { { "id", memberIds } };

            // Use GetByFilterAsync to get the list of members based on the filter
            var membersResult = await _memberRepository.SearchAllAsync(pageSize, pageNumber, filter);

            // Check if the operation was successful, and handle accordingly
            if (membersResult.IsSuccess && membersResult.Value is not null)
            {
                // Return the members as a List
                return membersResult;
            }

            // If there was an error or the result was null, return a failure result with an error message
            return ResultT<Tuple<int, IEnumerable<Member>>>.Failure(membersResult.Error ?? Error.NotFound("NoMembersFound", $"No members found for the club with ID {clubId}."));
        }
    }
}


