using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.PostgresDataAccess;
using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

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
