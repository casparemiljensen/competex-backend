using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{

    public class MockMatchRepository : MockGenericRepository<Match>, IMatchRepository
    {

        public MockMatchRepository(MockDatabaseManager db) : base(db)
        {
        }
    }
}
