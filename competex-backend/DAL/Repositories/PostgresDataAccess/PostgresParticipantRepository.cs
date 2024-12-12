using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresParticipantRepository : PostgresGenericRepository<Ekvipage>, IParticipantRepository
    {
        public PostgresParticipantRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        //public override async Task<ResultT<Tuple<int, IEnumerable<Ekvipage>>>> GetAllAsync(int? pageSize, int? pageNumber)
        //{
        //    // Step 1: Fetch participants with pagination
        //    var query = _dbSet.AsNoTracking(); // Participants query

        //    // Calculate total pages for pagination
        //    var totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, await query.CountAsync());

        //    // Fetch paginated participants
        //    var participants = await query
        //        .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
        //        .Take(pageSize ?? Defaults.PageSize)
        //        .ToListAsync();

        //    // Step 2: Fetch all participant-member relationships for the paginated participants
        //    var participantIds = participants.Select(p => p.Id).ToList(); // Get participant IDs
        //    var participantMembers = await _dbContext.ParticipantMembers
        //        .Where(pm => participantIds.Contains(pm.ParticipantId)) // Filter by relevant ParticipantIds
        //        .Select(pm => new { pm.ParticipantId, pm.MemberId }) // Composite key fields
        //        .ToListAsync();

        //    // Step 3: Map MemberIds to each participant
        //    foreach (var participant in participants)
        //    {
        //        // Ensure casting to DbParticipant
        //        if (participant is DbParticipant dbParticipant)
        //        {
        //            dbParticipant.MemberIds = participantMembers
        //                .Where(pm => pm.ParticipantId == dbParticipant.Id)
        //                .Select(pm => pm.MemberId)
        //                .ToList();
        //        }
        //    }

        //    //List<Participant> participantResult = ConvertDbParticipantsToParticipants(participants);

        //    // Step 4: Return paginated participants with MemberIds
        //    return ResultT<Tuple<int, IEnumerable<DbParticipant>>>.Success(
        //        new Tuple<int, IEnumerable<DbParticipant>>(totalPages, participants)
        //    );
        //}
    }
}
