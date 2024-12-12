using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresRoundRepository : PostgresGenericRepository<Round>, IRoundRepository
    {
        public PostgresRoundRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<ResultT<Tuple<int, IEnumerable<Round>>>> GetRoundIdsByCompetitionId(Guid CompetitionId, int? pageSize, int? pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
