using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresClubRepository : PostgresGenericRepository<Club>, IClubRepository
    {
        public PostgresClubRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}


