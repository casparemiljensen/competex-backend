using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresScoreRepository : PostgresGenericRepository<Score>, IScoreRepository
    {
        public PostgresScoreRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}


