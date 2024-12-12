using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresMatchRepository : PostgresGenericRepository<Match>, IMatchRepository
    {
        public PostgresMatchRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetMatchesByRoundId(Guid roundId, int? pageSize, int? pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}


