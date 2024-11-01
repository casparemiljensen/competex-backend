using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockGenericRepository<T> : IGenericRepository<T> where T : class, IIdentifiable
        // Stating that T is a reference type
    {

        public readonly List<T> _entities;

        public MockGenericRepository(MockDatabaseManager db)
        {
            _entities = db.GetEntities<T>();
        }

        // Retrieve a specific member by ID asynchronously
        public Task<T?> GetByIdAsync(Guid Id)
        {
            var member = _entities.FirstOrDefault(m => m.Id == Id);
            return Task.FromResult(member);
        }

        // Retrieve all members asynchronously
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(_entities.AsEnumerable());
        }



        //Add a new member asynchronously
        public async Task<Guid> InsertAsync(T obj)
        {
            // TODO: Whenever providing a GUID in post calls, it is ignored and a new GUID is generated.
            // Remove possibility to make it in UI.
            obj.Id = Guid.NewGuid();  // Generate a new Guid for new members
            try
            {
                _entities.Add(obj);
                await Task.CompletedTask; // Simulate async work
                return obj.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }


        // Update an existing member asynchronously

        public async Task<bool> UpdateAsync(T obj)
        {
            var existingRecord = _entities.FirstOrDefault(c => c.Id == obj.Id);
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

        // Delete a member by ID asynchronously
        public async Task<bool> DeleteAsync(Guid id)
        {
            var memberToRemove = _entities.FirstOrDefault(m => m.Id == id);
            if (memberToRemove != null)
            {
                try
                {
                    _entities.Remove(memberToRemove);
                    await Task.CompletedTask; // Simulate async work
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not delete member", ex);
                }
            }
            return false;
        }
    }
}
