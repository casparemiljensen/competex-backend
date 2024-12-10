using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresMemberRepository : PostgresGenericRepository<Member>, IMemberRepository
    {
        public PostgresMemberRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Member?> GetByFirstNameAsync(string firstName)
        {
            return await _dbSet.FirstOrDefaultAsync(l => l.FirstName.ToLower() == firstName.ToLowerInvariant());
        }
    }
}