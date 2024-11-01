//using competex_backend.DAL.Interfaces;
//using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockEntityRepository : MockGenericRepository<Entity>, IEntityRepository
    {
        public MockEntityRepository(MockDatabaseManager db) : base(db)
        {
        }

    }
}
