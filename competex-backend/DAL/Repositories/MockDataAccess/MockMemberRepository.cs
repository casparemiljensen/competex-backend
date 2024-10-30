using competex_backend.DAL.Interfaces;
using Member = competex_backend.Models.Member;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockMemberRepository : MockGenericRepository<Member>, IMemberRepository
    {

        public MockMemberRepository(MockDatabaseManager db) : base(db)
        {
        }

        public Task<Member?> GetByFirstNameAsync(string firstName)
        {
            var member = _entities.FirstOrDefault(m => m.FirstName == firstName);
            return Task.FromResult(member);
        }
    }
}
