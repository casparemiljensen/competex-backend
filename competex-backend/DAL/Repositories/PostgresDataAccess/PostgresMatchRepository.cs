using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresMatchRepository : PostgresGenericRepository<Match>, IMatchRepository
    {
        public PostgresMatchRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetMatchesByRoundId(Guid roundId, int? pageSize, int? pageNumber)
        {
            // Filter matches by RoundId
            var query = _dbContext.Matches.Where(match => match.RoundId == roundId);

            // Count total matches for pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var paginatedMatches = await query
                .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
                .Take(pageSize ?? Defaults.PageSize)
                .ToListAsync();

            // Calculate total pages
            var totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, totalCount);

            // Return result
            return ResultT<Tuple<int, IEnumerable<Match>>>.Success(
                new Tuple<int, IEnumerable<Match>>(totalPages, paginatedMatches));
        }
    }
}


