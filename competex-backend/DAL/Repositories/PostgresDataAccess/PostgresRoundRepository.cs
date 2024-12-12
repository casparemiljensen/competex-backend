using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresRoundRepository : PostgresGenericRepository<Round>, IRoundRepository
    {
        public PostgresRoundRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<ResultT<Tuple<int, IEnumerable<Round>>>> GetRoundIdsByCompetitionId(Guid competitionId, int? pageSize, int? pageNumber)
        {
            // Filter rounds by CompetitionId
            var query = _dbContext.Rounds.Where(round => round.CompetitionId == competitionId);

            // Count total rounds for pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var paginatedRounds = await query
                .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
                .Take(pageSize ?? Defaults.PageSize)
                .ToListAsync();

            // Calculate total pages
            var totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, totalCount);

            // Return result
            return ResultT<Tuple<int, IEnumerable<Round>>>.Success(
                new Tuple<int, IEnumerable<Round>>(totalPages, paginatedRounds));
        }
    }
}