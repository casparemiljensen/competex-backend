using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresCompetitionTypeRepository : PostgresGenericRepository<CompetitionType>, ICompetitionTypeRepository
    {
        public PostgresCompetitionTypeRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
