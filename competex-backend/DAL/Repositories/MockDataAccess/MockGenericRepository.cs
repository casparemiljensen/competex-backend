using AutoMapper.Internal;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Filters;
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
            var entities = await Task.Run(() => _entities);
            List<T> filtertedEntities = [];
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.Value is JsonElement jsonElement)
                    {
                        if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var filterEntity in jsonElement.EnumerateArray())
                            {
                                Console.WriteLine(filterEntity);
                                var filteredResult = InsertName(filter.Key, filterEntity, entities);
                                if (!filteredResult.IsSuccess)
                                {
                                    return ResultT<Tuple<int, IEnumerable<T>>>.Failure(filteredResult.Error!);
                                }
                                filtertedEntities.AddRange(filteredResult.Value);
                            }
                        }
                        else
                        {
                            var filteredResult = InsertName(filter.Key, filter.Value, entities);
                            if (!filteredResult.IsSuccess)
                            {
                                return ResultT<Tuple<int, IEnumerable<T>>>.Failure(filteredResult.Error!);
                            }
                            filtertedEntities.AddRange(filteredResult.Value);
                        }
                    }
                    if (filter.Value is IEnumerable enumerable)
                    {
                        foreach (var filterEntity in enumerable)
                        {
                            Console.WriteLine(filterEntity);
                            var filteredResult = InsertName(filter.Key, filterEntity, entities);
                            if (!filteredResult.IsSuccess)
                            {
                                return ResultT<Tuple<int, IEnumerable<T>>>.Failure(filteredResult.Error!);
                            }
                            filtertedEntities.AddRange(filteredResult.Value);
                        }
                    }
                }
            }
            else
            {
                filtertedEntities = _entities;
            }

            int totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, filtertedEntities.Count());

            var result = filtertedEntities.Distinct()
            .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
            .Take(pageSize ?? Defaults.PageSize);
            return ResultT<Tuple<int, IEnumerable<T>>>.Success(new Tuple<int, IEnumerable<T>>(totalPages, result));
        }

        private ResultT<IEnumerable<T>> InsertName(string filterKey, object filterValue, List<T> entities)
        {
            Console.WriteLine(filterKey);
            Console.WriteLine(filterValue.ToString());
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

            if (propertyInfo != null)
            {
                if (propertyInfo.GetValue(entities[0]) is IEnumerable)
                {
                    return ResultT<IEnumerable<T>>.Success(entities
                        .Where(entity =>
                        {
                            var propertyValue = propertyInfo.GetValue(entity);
                            return (propertyValue as IEnumerable)!.Cast<object>()
                                .Any(item => item.ToString()?.Trim().Replace("\"", "") == serializedFilterValue.ToString().Trim().Replace("\"", ""));
                        }));
                    /*
                    filtertedEntities.AddRange(
                    entities
                        .Where(entity =>
                        {
                            var propertyValue = propertyInfo.GetValue(entity);
                            return (propertyValue as IEnumerable)!.Cast<object>()
                                .Any(item => item.ToString()?.Trim().Replace("\"", "") == serializedFilterValue.ToString().Trim().Replace("\"", ""));
                        })
                    );*/
                }
                else
                {
                    foreach (var entity in entities)
                    {
                        Console.WriteLine(propertyInfo!.GetValue(entity)!.ToString()!.Trim() + ":" + serializedFilterValue.ToString().Trim().Replace("\"", ""));

                    }
                    return ResultT<IEnumerable<T>>.Success(entities.Where(entity =>
                            propertyInfo!.GetValue(entity)!.ToString()!.Trim() == serializedFilterValue.ToString().Trim().Replace("\"", "")));/*
                    filtertedEntities.AddRange(entities.Where(entity =>
                            propertyInfo!.GetValue(entity)!.ToString()!.Trim() == serializedFilterValue.ToString().Trim().Replace("\"", "")));*/
                }
            }
            else
            {
                return ResultT<IEnumerable<T>>.Failure(Error.FilterError("FilterError", $"Could not find a property with name {filterKey}"));
            }
        }


        // Add a new entity
        public async Task<ResultT<Guid>> InsertAsync(T obj)
        {
            // TODO: Whenever providing a GUID in post calls, it is ignored and a new GUID is generated.
            // Remove possibility to make it in UI.
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
