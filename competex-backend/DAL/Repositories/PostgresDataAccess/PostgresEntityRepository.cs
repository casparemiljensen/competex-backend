using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresEntityRepository : PostgresGenericRepository<Entity>, IEntityRepository
    {
        public PostgresEntityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}