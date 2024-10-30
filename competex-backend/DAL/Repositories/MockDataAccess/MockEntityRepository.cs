using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockEntityRepository : IEntityRepository
    {
        private readonly IDatabaseManager _db;

        public MockEntityRepository(IDatabaseManager db)
        {
            _db = db;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var recordToRemove = _db.Entities.FirstOrDefault(c => c.EntityId == id);
            if (recordToRemove != null)
            {
                try
                {
                    _db.Entities.Remove(recordToRemove);
                    await Task.CompletedTask; // Simulate async work
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not delete entity'", ex);
                }
            }
            return false;
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await Task.FromResult(_db.Entities);
        }

        public async Task<Entity?> GetByIdAsync(Guid id)
        {
            var entity = _db.Entities.FirstOrDefault(c => c.EntityId== id);
            return await Task.FromResult(entity);
        }

        public async Task<Guid> InsertAsync(Entity obj)
        {
            obj.EntityId = Guid.NewGuid();  // Generate a new Guid for new clubs
            try
            {
                _db.Entities.Add(obj);
                await Task.CompletedTask;
                return obj.EntityId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> UpdateAsync(Entity obj)
        {
            var existingRecord = _db.Entities.FirstOrDefault(c => c.EntityId == obj.EntityId);
            if (existingRecord != null)
            {
                existingRecord = obj; // Set existing record to new record
                await Task.CompletedTask; // Simulate async work
                return true;
                //existingClub.Organizers = club.Organizers;
                //existingClub.ClubMembers = club.ClubMembers;
            }
            return false;
        }
    }
}
