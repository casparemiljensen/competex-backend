using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockLocationRepository : MockGenericRepository<Location>, ILocationRepository
    {
        public MockLocationRepository(MockDatabaseManager db)
            : base(db)
        {
        }
    }
}