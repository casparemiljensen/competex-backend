using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresScoreResultRepository : PostgresGenericRepository<ScoreResult>, IScoreResultRepository
    {
        public PostgresScoreResultRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}


