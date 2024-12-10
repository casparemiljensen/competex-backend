using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresCompetitionRepository : PostgresGenericRepository<Competition>, ICompetitionRepository
    {
        public PostgresCompetitionRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
