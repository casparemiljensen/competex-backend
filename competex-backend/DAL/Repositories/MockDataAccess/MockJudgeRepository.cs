using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{

    public class MockJudgeRepository : MockGenericRepository<Judge>, IJudgeRepository
    {
        public MockJudgeRepository(MockDatabaseManager db)
            : base(db)
        {
        }
    }
}
