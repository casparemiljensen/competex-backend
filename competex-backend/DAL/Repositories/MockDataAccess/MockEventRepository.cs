using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockEventRepository : MockGenericRepository<Event>, IEventRepository
    {
        public MockEventRepository(MockDatabaseManager db) : base(db)
        {
        }
    }
}