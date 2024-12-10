using competex_backend.Common.ErrorHandling;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Collections;
using System.Reflection;
using System.Text.Json;

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

        public async Task<ResultT<Tuple<int, IEnumerable<T>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var entities = await Task.Run(() => _entities);

            var totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, entities.Count);

            var result = entities
            .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
            .Take(pageSize ?? Defaults.PageSize);
            return ResultT<Tuple<int, IEnumerable<T>>>.Success(new Tuple<int, IEnumerable<T>>(totalPages, result));
        }

        public async Task<ResultT<Tuple<int, IEnumerable<T>>>> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters)
        {
            List<T> filtertedEntities = await Task.Run(() => _entities);
            if (filters == null)
            {
                filtertedEntities = _entities;
            }
            else
            {
                foreach (var filter in filters)
                {
                    List<T> orList = [];
                    if (filter.Value is JsonElement jsonElement)
                    {
                        if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var filterEntity in jsonElement.EnumerateArray())
                            {
                                orList.AddRange(GetAllMatching(filter.Key, filterEntity, filtertedEntities).ToList());
                            }
                        }
                        else
                        {
                            orList = GetAllMatching(filter.Key, filter.Value, filtertedEntities).ToList();
                        }
                    }
                    else if (filter.Value is IEnumerable enumerable)
                    {
                        foreach (var filterEntity in enumerable)
                        {
                            orList.AddRange(GetAllMatching(filter.Key, filterEntity, filtertedEntities));
                        }
                    }
                    else if (filter.Value.GetType().IsAssignableTo(typeof(string)) || filter.Value is Guid)
                    {
                        orList.AddRange(GetAllMatching(filter.Key, filter.Value, filtertedEntities));
                    }
                    else
                    {
                        //Unrecognised type gets handed to the void
                        throw new ApiException(500, $"Type not found {filter.Value}");
                    }
                    //Set orList to filteredEntities for potentially more "and" properties
                    filtertedEntities = orList;
                }
            }

            int totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, filtertedEntities.Count());

            var result = filtertedEntities.Distinct()
            .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
            .Take(pageSize ?? Defaults.PageSize).ToList();
            return ResultT<Tuple<int, IEnumerable<T>>>.Success(new Tuple<int, IEnumerable<T>>(totalPages, result));
        }

        private IEnumerable<T> GetAllMatching(string filterKey, object filterValue, List<T> entities)
        {
            // Convert filter value to string if necessary (in case it is a JsonElement or other type)
            if (filterValue is JsonElement jsonElement)
            {
                filterValue = jsonElement.ToString();
            }
            DateTime time;
            if (DateTime.TryParse(filterValue.ToString(), out time))
            {
                filterValue = time.ToString();
            }

            var serializedFilterValue = JsonSerializer.Serialize(filterValue);

            var propertyInfo = typeof(T).GetProperty(filterKey, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new ApiException(500, $"No property on {typeof(T).Name} called {filterKey}");
            }

            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)) && typeof(T) != typeof(string))
            {
                return entities
                    .Where(entity =>
                    {
                        var propertyValue = propertyInfo.GetValue(entity);
                        return (propertyValue as IEnumerable)!.Cast<object>()
                            .Any(item => item.ToString()?.Trim().Replace("\"", "") == serializedFilterValue.ToString().Trim().Replace("\"", ""));
                    });
            }
            else
            {
                return entities.Where(entity =>
                        propertyInfo!.GetValue(entity)!.ToString()!.Trim() == serializedFilterValue.ToString().Trim().Replace("\"", ""));
            }
        }


        // Add a new entity
        public virtual async Task<ResultT<Guid>> InsertAsync(T obj)
        {
            obj.Id = Guid.NewGuid();  // Generate a new Guid for new clubs
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
            int index = await Task.Run(() => _entities.FindIndex(o => o.Id == id));
            if (index == -1)
            {
                return Result.Failure(Error.NotFound("NotFound", $"{typeof(T).Name.ToLower()} with ID {obj.Id} does not exist."));
            }

            try
            {
                await Task.Run(() =>
                {
                    var id = _entities[index].Id; // Keep the original id
                    _entities[index] = obj; // Replace the object directly
                    _entities[index].Id = id;
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
    }
}
