using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresFieldRepository : PostgresGenericRepository<Field>, IFieldRepository
    {
        public PostgresFieldRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
