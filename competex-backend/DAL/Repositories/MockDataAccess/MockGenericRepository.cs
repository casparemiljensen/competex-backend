using competex_backend.DAL.Filters;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Linq.Expressions;

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
                : ResultT<T>.Failure(Error.NotFound($"{typeof(T).Name.ToLower()} not found.", $"{typeof(T).Name.ToLower()} with ID {id} does not exist."));
        }

        public async Task<ResultT<IEnumerable<T>>> GetAllAsync(BaseFilter? filter = null)
        {
            if (filter is null)
            {
                var entities = await Task.Run(() => _entities.ToList());
                return ResultT<IEnumerable<T>>.Success(entities);
            }

            try
            {
                // Initialize a queryable list from the in-memory collection of entities.
                var query = _entities.AsQueryable();

                // Apply filtering if a list of IDs is provided in the filter and contains elements.
                if (filter.Ids is not null)
                {
                    // Restrict the query to entities whose IDs are in the provided list.
                    query = query.Where(entity => filter.Ids.Contains(entity.Id));
                }

                // Run the query asynchronously and retrieve the results as a list.
                var filteredEntities = await Task.Run(() => query.ToList());

                // Return the result as a successful operation containing the filtered entities.
                return ResultT<IEnumerable<T>>.Success(filteredEntities);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a failure result with an error message indicating the issue.
                return ResultT<IEnumerable<T>>.Failure(Error.FilterError("FilterError", $"Failed to filter {typeof(T).Name.ToLower()}: {ex.Message}"));
            }
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
        public async Task<Result> UpdateAsync(T obj)
        {
            int index = await Task.Run(() => _entities.FindIndex(m => m.Id == obj.Id));
            if (index == -1)
            {
                return Result.Failure(Error.NotFound("NotFound", $"{typeof(T).Name.ToLower()} with ID {obj.Id} does not exist."));
            }

            try
            {
                await Task.Run(() =>
                {
                    _entities[index] = obj; // Replace the object directly
                }); // Simulate async work

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("UpdateError", $"Failed to update {typeof(T).Name.ToLower()}: {ex.Message}"));
            }
        }


        // Delete an entity
        public async Task<Result> DeleteAsync(Guid id)
        {
            var entityToRemove = await Task.Run(() => _entities.FirstOrDefault(c => c.Id == id));
            if (entityToRemove is null)
            {
                return Result.Failure(Error.NotFound("NotFound", $"{typeof(T).Name.ToLower()} with ID {id} does not exist."));
            }

            try
            {
                await Task.Run(() => _entities.Remove(entityToRemove)); // Simulate async work
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("DeletionError", $"Could not delete {typeof(T).Name.ToLower()}: {ex.Message}"));
            }
        }

        // New filter method
        //public async Task<ResultT<IEnumerable<T>>> GetByFilterAsync(BaseFilter filter)
        //{
        //    try
        //    {
        //        // Initialize a queryable list from the in-memory collection of entities.
        //        var query = _entities.AsQueryable();

        //        // Apply filtering if a list of IDs is provided in the filter and contains elements.
        //        if (filter.Ids is not null && filter.Ids.Any())
        //        {
        //            // Restrict the query to entities whose IDs are in the provided list.
        //            query = query.Where(entity => filter.Ids.Contains(entity.Id));
        //        }

        //        // Run the query asynchronously and retrieve the results as a list.
        //        var filteredEntities = await Task.Run(() => query.ToList());

        //        // Return the result as a successful operation containing the filtered entities.
        //        return ResultT<IEnumerable<T>>.Success(filteredEntities);
        //    }
        //    catch (Exception ex)
        //    {
        //        // If an error occurs, return a failure result with an error message indicating the issue.
        //        return ResultT<IEnumerable<T>>.Failure(Error.FilterError("FilterError", $"Failed to filter {typeof(T).Name.ToLower()}: {ex.Message}"));
        //    }
        //}
    }
}
