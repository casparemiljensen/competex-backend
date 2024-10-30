using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubRepository : MockGenericRepository<Club>, IClubRepository
    {
        public MockClubRepository(MockDatabaseManager db) : base(db)
        {
        }

            public IEnumerable<Club> GetClubByName(string name)
        {
            var clubs = _entities.Where(c => c.Name == name).ToList();
            return clubs;
        }
    }
}
