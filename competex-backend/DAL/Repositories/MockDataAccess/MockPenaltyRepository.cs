using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockPenaltyRepository : MockGenericRepository<Penalty>, IPenaltyRepository
    {
        public MockPenaltyRepository(MockDatabaseManager db) : base(db)
        {
        }
    }
}
