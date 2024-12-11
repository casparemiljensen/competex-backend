using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresParticipantRepository : PostgresGenericRepository<Ekvipage>, IParticipantRepository
    {
        public PostgresParticipantRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
