using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresRegistrationRepository : PostgresGenericRepository<Registration>, IRegistrationRepository
    {
        public PostgresRegistrationRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
