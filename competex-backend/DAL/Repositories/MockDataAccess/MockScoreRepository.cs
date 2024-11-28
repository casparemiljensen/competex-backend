using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockScoreRepository : MockGenericRepository<Score>, IScoreRepository
    {
        public MockScoreRepository(MockDatabaseManager db) : base(db)
        {
        }
    }
}
