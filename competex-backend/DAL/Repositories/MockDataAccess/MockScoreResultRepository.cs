using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockScoreResultRepository : MockGenericRepository<ScoreResult>, IScoreResultRepository
    {
        public MockScoreResultRepository(MockDatabaseManager db)
            : base(db)
        {
        }
    }
}
