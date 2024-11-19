using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockAdminRepository : MockGenericRepository<Admin>, IAdminRepository
    {
        public MockAdminRepository(MockDatabaseManager db)
            : base(db)
        {
        }
    }
}
