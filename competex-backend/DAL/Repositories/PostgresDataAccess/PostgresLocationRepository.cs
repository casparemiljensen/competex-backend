using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresLocationRepository : PostgresGenericRepository<Location>, ILocationRepository
    {
        public PostgresLocationRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        // Example of a custom method
        public async Task<Location?> GetLocationByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(l => l.Name.ToLower() == name.ToLowerInvariant());
        }
    }
}