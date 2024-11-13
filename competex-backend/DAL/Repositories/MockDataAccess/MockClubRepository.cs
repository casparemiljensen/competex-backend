using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubRepository : MockGenericRepository<Club>, IClubRepository
    {
        public MockClubRepository(MockDatabaseManager db) : base(db)
        {
        }

        public async Task<ResultT<IEnumerable<Club>>> GetClubsByNameAsync(string name)
        {
            // Simulate an asynchronous operation
            var clubs = await Task.Run(() => _entities.Where(c => c.Name == name).ToList());

            // Return the results wrapped in ResultT
            return ResultT<IEnumerable<Club>>.Success(clubs);

        }


    }
}
