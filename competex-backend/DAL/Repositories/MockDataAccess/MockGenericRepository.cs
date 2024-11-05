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

        // Get entity by ID
        public async Task<ResultT<T>> GetByIdAsync(Guid id) // If problems then make T nullable
        {
            var entity = await Task.Run(() => _entities.FirstOrDefault(c => c.Id == id));
            return entity is not null
                ? ResultT<T>.Success(entity)
                : ResultT<T>.Failure(Error.NotFound($"{typeof(T).ToString().ToLower()} not found.", $"{typeof(T).ToString().ToLower()} with ID {id} does not exist."));
        }

        public async Task<ResultT<IEnumerable<T>>> GetAllAsync()
        {
            var entities = await Task.Run(() => _entities.ToList());
            return ResultT<IEnumerable<T>>.Success(entities);
        }



        // Add a new entity
        public async Task<ResultT<Guid>> InsertAsync(T obj)
        {
            // TODO: Whenever providing a GUID in post calls, it is ignored and a new GUID is generated.
            // Remove possibility to make it in UI.
            // obj.Id = Guid.NewGuid();  // Generate a new Guid for new clubs
            try
            {
                await Task.Run(() => _entities.Add(obj)); // Simulate async work
                return ResultT<Guid>.Success(obj.Id);
            }
            catch (Exception ex)
            {
                return ResultT<Guid>.Failure(Error.Failure("InsertionError", $"Failed to insert {typeof(T).ToString().ToLower()}: {ex.Message}"));
            }
        }



        // Update an existing entity
        public async Task<Result> UpdateAsync(Guid id, T obj)
        {
            int index = await Task.Run(() => _entities.FindIndex(m => m.Id == id));
            if (index == -1)
            {
                return Result.Failure(Error.NotFound("NotFound", $"{typeof(T).ToString().ToLower()} with ID {id} does not exist."));
            }

            try
            {
                await Task.Run(() =>
                {
                    var target = _entities[index];

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        var propertyName = prop.Name;
                        var propertyValue = prop.GetValue(obj);

                        // Set the property on the target object using reflection
                        var targetProp = target.GetType().GetProperty(propertyName);
                        if (targetProp != null && targetProp.CanWrite)  // Ensure property exists and is writable
                        {
                            targetProp.SetValue(target, propertyValue);
                        }
                    }
                }); // Simulate async work

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("UpdateError", $"Failed to update {typeof(T).ToString().ToLower()}: {ex.Message}"));
            }
        }


        // Delete an entity
        public async Task<Result> DeleteAsync(Guid id)
        {
            var entityToRemove = await Task.Run(() => _entities.FirstOrDefault(c => c.Id == id));
            if (entityToRemove is null)
            {
                return Result.Failure(Error.NotFound("NotFound", $"{typeof(T).ToString().ToLower()} with ID {id} does not exist."));
            }

            try
            {
                await Task.Run(() => _entities.Remove(entityToRemove)); // Simulate async work
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("DeletionError", $"Could not delete {typeof(T).ToString().ToLower()}: {ex.Message}"));
            }
        }
    }
}
