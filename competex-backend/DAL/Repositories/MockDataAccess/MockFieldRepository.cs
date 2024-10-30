using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockFieldRepository : IFieldRepository
    {
        private readonly IDatabaseManager _db;
        public MockFieldRepository(IDatabaseManager db)
        {
            _db = db;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var recordToRemove = _db.Fields.FirstOrDefault(c => c.FieldId == id);
            if (recordToRemove != null)
            {
                try
                {
                    _db.Fields.Remove(recordToRemove);
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

        public async Task<IEnumerable<Field>> GetAllAsync()
        {
            return await Task.FromResult(_db.Fields);
        }

        public async Task<Field?> GetByIdAsync(Guid id)
        {
            var record = _db.Fields.FirstOrDefault(c => c.FieldId == id);
            return await Task.FromResult(record);
        }

        public async Task<Guid> InsertAsync(Field obj)
        {
            obj.FieldId = Guid.NewGuid();  // Generate a new Guid 
            try
            {
                _db.Fields.Add(obj);
                await Task.CompletedTask;
                return obj.FieldId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> UpdateAsync(Field obj)
        {
            var existingRecord = _db.Fields.FirstOrDefault(c => c.FieldId == obj.FieldId);
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
