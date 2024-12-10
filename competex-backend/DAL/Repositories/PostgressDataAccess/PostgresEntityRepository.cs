using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.PostgresDataAccess;
using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Models;

public class PostgresEntityRepository : PostgresGenericRepository<Entity>, IEntityRepository
{
    public PostgresEntityRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
