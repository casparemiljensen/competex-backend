using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.PostgresDataAccess;
using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Models;

public class PostgresJudgeRepository : PostgresGenericRepository<Judge>, IJudgeRepository
{
    public PostgresJudgeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
