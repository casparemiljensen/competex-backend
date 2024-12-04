using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{

    public class MockMatchRepository : MockGenericRepository<Match>, IMatchRepository
    {

        public MockMatchRepository(MockDatabaseManager db) : base(db)
        {
        }

        public async Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetMatchesByRoundId(Guid roundId, int? pageSize, int? pageNumber)
        {
            var entities = await Task.Run(() => _entities.Where(match => match.RoundId == roundId).ToList());

            // Apply pagination asynchronously
            var paginatedMatches = await Task.Run(() => entities
                .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
                .Take(pageSize ?? Defaults.PageSize));

            return ResultT<Tuple<int, IEnumerable<Match>>>.Success(
                new Tuple<int, IEnumerable<Match>>(
                    PaginationHelper.GetTotalPages(pageSize, pageNumber, entities.Count),
                    paginatedMatches));
        }
    }
}
