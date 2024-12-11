using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresJudgeRepository : PostgresGenericRepository<Judge>, IJudgeRepository
    {
        public PostgresJudgeRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}