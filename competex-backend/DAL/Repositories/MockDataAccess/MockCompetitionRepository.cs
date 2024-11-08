using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockCompetitionRepository : MockGenericRepository<Competition>, ICompetitionRepository
    {
        public MockCompetitionRepository(MockDatabaseManager db) : base(db)
        {
        }
    }
}