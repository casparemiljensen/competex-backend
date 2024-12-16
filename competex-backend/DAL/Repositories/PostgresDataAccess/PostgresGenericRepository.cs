using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections;
using System.Text.Json;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresGenericRepository<T> : IGenericRepository<T> where T : Identifiable
    {
        public readonly ApplicationDbContext _dbContext;
        public readonly DbSet<T> _dbSet;

        public PostgresGenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        // TODO: Remove all newGuids for PG
        // Get entity by ID
        public virtual async Task<ResultT<T>> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(c => c.Id == id);
            return entity is not null
                ? ResultT<T>.Success(entity)
                : ResultT<T>.Failure(Error.NotFound($"{typeof(T).Name.ToLower()} not found.", $"{typeof(T).Name.ToLower()} with ID {id} does not exist."));
        }

        // Get all entities with pagination
        public virtual async Task<ResultT<Tuple<int, IEnumerable<T>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var query = _dbSet.AsNoTracking(); // TODO: Remove globally

            var totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, await query.CountAsync());
            var result = await query
                .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
                .Take(pageSize ?? Defaults.PageSize)
                .ToListAsync();

            return ResultT<Tuple<int, IEnumerable<T>>>.Success(new Tuple<int, IEnumerable<T>>(totalPages, result));
        }


        public async Task<ResultT<Tuple<int, IEnumerable<T>>>> SearchAllAsync(
            int? pageSize, int? pageNumber, Dictionary<string, object>? filters)
        {
            // Generate the SQL query and parameters
            var (query, parameters) = BuildSearchQuery(DatabaseHelper.GetTableName<T>(), filters ?? new Dictionary<string, object>());

            // Execute the raw SQL query
            var resultSet = _dbSet.FromSqlRaw(query, parameters.ToArray());
            Console.WriteLine(filters.Count());
            Console.WriteLine(query);
            // Count total records
            var totalRecords = await resultSet.CountAsync();

            // Apply pagination manually (if needed)
            if (pageSize.HasValue && pageNumber.HasValue)
            {
                resultSet = resultSet.Skip((pageNumber.Value - 1) * pageSize.Value)
                                     .Take(pageSize.Value);
            }

            // Fetch results
            var results = await resultSet.ToListAsync();

            // Wrap results in the expected return type
            return ResultT<Tuple<int, IEnumerable<T>>>.Success(
                new Tuple<int, IEnumerable<T>>(totalRecords, results));
        }

        // Add a new entity
        public virtual async Task<ResultT<Guid>> InsertAsync(T obj)
        {
            //obj.Id = Guid.NewGuid(); // Generate a new Guid
            try
            {
                await _dbSet.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                return ResultT<Guid>.Success(obj.Id);
            }
            catch (Exception ex)
            {
                return ResultT<Guid>.Failure(Error.Failure("InsertionError", $"Failed to insert {typeof(T).Name.ToLower()}: {ex.Message} - {ex.InnerException.Message}"));
            }
        }


        // Update an existing entity
        public async Task<Result> UpdateAsync(Guid id, T obj)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            if (existingEntity is null)
            {
                return Result.Failure(Error.NotFound("NotFound", $"{typeof(T).Name.ToLower()} with ID {id} does not exist."));
            }

            try
            {
                // Iterate over the properties and update manually, excluding Id
                var entry = _dbContext.Entry(existingEntity);
                foreach (var property in entry.Metadata.GetProperties())
                {
                    if (property.Name == nameof(existingEntity.Id))
                        continue; // Skip the Id property

                    // Copy property value from obj to existingEntity
                    var newValue = entry.Property(property.Name).CurrentValue = obj.GetType().GetProperty(property.Name)?.GetValue(obj);
                    entry.Property(property.Name).IsModified = true;
                }

                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("UpdateError", $"Failed to update {typeof(T).Name.ToLower()}: {ex.Message}"));
            }
        }

        // Delete an entity
        public virtual async Task<Result> DeleteAsync(Guid id)
        {
            var entityToRemove = await _dbSet.FirstOrDefaultAsync(c => c.Id == id);
            if (entityToRemove is null)
            {
                return Result.Failure(Error.NotFound("NotFound", $"{typeof(T).Name.ToLower()} with ID {id} does not exist."));
            }

            try
            {
                _dbSet.Remove(entityToRemove);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("DeletionError", $"Could not delete {typeof(T).Name.ToLower()}: {ex.Message}"));
            }
        }


        public static (string query, List<NpgsqlParameter> parameters) BuildSearchQuery(string tableName, Dictionary<string, object> filters)
        {
            var orConditions = new List<string>();
            int queryIndex = 0;
            List<NpgsqlParameter> paramList = [];

            foreach (var filter in filters)
            {
                var filterKey = string.Concat(filter.Key[0].ToString().ToUpper(), filter.Key.AsSpan(1));
                if (!IsValidSQLString(filterKey))
                {
                    Console.WriteLine("Banned character used");
                    throw new InvalidOperationException("Banned character used");
                }
                if (filter.Value is JsonElement jsonElement)
                {
                    if (jsonElement.ValueKind == JsonValueKind.Array)
                    {
                        int arrayLength = jsonElement.GetArrayLength();
                        List<string> or = [];
                        foreach (var filterEntity in jsonElement.EnumerateArray())
                        {
                            if (!paramList.AddTypeCorrectFilter(filterEntity)) continue;
                            or.Add($"\"{filterKey}\" = {{{queryIndex}}}");
                            queryIndex++;
                        }
                        orConditions.Add(string.Join(" OR ", or));
                        continue;
                    }
                    else
                    {
                        if (!paramList.AddTypeCorrectFilter(filter.Value)) continue;
                        orConditions.Add($"\"{filterKey}\" = {{{queryIndex}}}");
                        queryIndex++;
                    }
                }
                else if (filter.Value is IEnumerable enumerable && filter.Value is not string)
                {
                    int arrayLength = enumerable.Cast<object>().Count();
                    List<string> or = [];
                    foreach (var filterEntity in enumerable)
                    {
                        if (!paramList.AddTypeCorrectFilter(filterEntity)) continue;
                        or.Add($"\"{filterKey}\" = {{{queryIndex}}}");
                        queryIndex++;
                    }
                    orConditions.Add(string.Join(" OR ", or));
                }
                else
                {
                    if (!paramList.AddTypeCorrectFilter(filter.Value)) continue;
                    orConditions.Add($"\"{filterKey}\" = {{{queryIndex}}}");
                    queryIndex++;
                }
            }
            orConditions.RemoveAll(x => x.Equals(string.Empty));

            // Combine conditions with AND
            string whereClause = orConditions.Count > 0 ? $"WHERE ({string.Join(") AND (", orConditions)})" : "";
            string query = $"SELECT * FROM \"{tableName}\" {whereClause}".Trim();
            return (query, paramList);
        }

        public static bool IsValidSQLString(string name)
        {
            // Basic validation: check for allowed characters
            return name.All(c => char.IsLetterOrDigit(c) || c == '_');
        }

    }
}