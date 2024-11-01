using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockFieldRepository : MockGenericRepository<Field>, IFieldRepository
    {
        public MockFieldRepository(MockDatabaseManager db)
            :base(db)
        {
        }
        
    }
}
