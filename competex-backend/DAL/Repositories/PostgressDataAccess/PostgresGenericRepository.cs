

using competex_backend.Common.ErrorHandling;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;
using System.Collections;
using System.Linq;
using Npgsql.Replication;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public class PostgresGenericRepository<T> : IGenericRepository<T> where T : class, IIdentifiable, IMappable<T>
    {
        public async Task<ResultT<Tuple<int, IEnumerable<T>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            string tableName = GetTableName();

            await using var command = new NpgsqlCommand($"SELECT * FROM \"{tableName}\";", PostgresConnection.conn);

            await using var reader = await command.ExecuteReaderAsync();

            return PaginationHelper.PaginationWrapper<T>(await SearchHelper.IterateOverReader<T>(reader), pageSize, pageNumber);
        }

        internal static string GetTableName()
        {
            switch (Activator.CreateInstance<T>())
            {
                case Member:
                    return "Member";
                case Match:
                    return "Match";
                default:
                    throw new ApiException(400, "Bad request: Invalid type requested");
            }
        }

        public async Task<ResultT<T>> GetByIdAsync(Guid id)
        {
            string tableName = GetTableName();

            //using var cmd = new NpgsqlCommand($"SELECT * FROM member WHERE id = '58a01cc0-1a49-455b-998c-1500b3db0dca'", PostgresConnection.conn)
            await using var cmd = new NpgsqlCommand($"SELECT * FROM \"{tableName}\" WHERE \"Id\" = ($1)", PostgresConnection.conn)
            {
                Parameters =
                {
                    new() { Value = id, NpgsqlDbType = NpgsqlDbType.Uuid},
                }
            };

            await using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return ResultT<T>.Failure(Error.NotFound("400", "Item with given id not found"));
            }

            return ResultT<T>.Success(await T.Map(reader));
        }

        public async Task<ResultT<Tuple<int, IEnumerable<T>>>> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters)
        {
            var tableName = GetTableName();

            var (query, parameters) = BuildSearchQuery(tableName, filters ?? []);

            await using var cmd = new NpgsqlCommand(query, PostgresConnection.conn);

            for (int i = 0; i < parameters.Count; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }

            await using var reader = await cmd.ExecuteReaderAsync();

            return PaginationHelper.PaginationWrapper<T>(await SearchHelper.IterateOverReader<T>(reader), pageSize, pageNumber);
        }


        public (string query, List<NpgsqlParameter> parameters) BuildSearchQuery(string tableName, Dictionary<string, object> filters)
        {
            var orConditions = new List<string>();
            int queryIndex = 1;
            List<NpgsqlParameter> paramList = [];

            foreach (var filter in filters)
            {
                if (!PostgresConnection.IsValidSQLString(filter.Key))
                {
                    Console.WriteLine("Banned character used");
                    break;
                }
                if (filter.Value is JsonElement jsonElement)
                {
                    if (jsonElement.ValueKind == JsonValueKind.Array)
                    {
                        Console.WriteLine("Is Array");
                        int arrayLength = jsonElement.GetArrayLength();
                        List<string> or = [];
                        foreach (var filterEntity in jsonElement.EnumerateArray())
                        {
                            Console.WriteLine("in loop");
                            or.Add($"\"{filter.Key}\" = ${queryIndex}");
                            paramList.AddTypeCorrectFilter(filterEntity);
                            queryIndex++;
                        }
                        orConditions.Add(string.Join(" OR ", or));
                        continue;
                    }
                    else
                    {
                        orConditions.Add($"\"{filter.Key}\" = ${queryIndex}");
                        paramList.AddTypeCorrectFilter(filter.Value);
                        queryIndex++;
                    }
                }
                else if (filter.Value is IEnumerable enumerable)
                {
                    int arrayLength = enumerable.Cast<object>().Count();
                    List<string> or = [];
                    foreach (var filterEntity in enumerable)
                    {
                        or.Add($"\"{filter.Key}\" = ${queryIndex}");
                        paramList.AddTypeCorrectFilter(filterEntity);
                        queryIndex++;
                    }
                    orConditions.Add(string.Join(" OR ", or));
                }
                else
                {
                    orConditions.Add($"\"{filter.Key}\" = ${queryIndex}");
                    paramList.AddTypeCorrectFilter(filter.Value);
                    queryIndex++;
                }
            }

            // Combine conditions with AND
            string whereClause = orConditions.Count > 0 ? $"WHERE ({string.Join(") AND (", orConditions)})" : "";
            string query = $"SELECT * FROM \"{tableName}\" {whereClause};";

            Console.WriteLine(query);

            return (query, paramList);
        }



        public async Task<ResultT<Guid>> InsertAsync(T obj)
        {
            string tableName = GetTableName();
            var internalGuid = Guid.Empty;
            var (columns, values) = obj.GetInsertSQLObject();

            var columnsNames = String.Join("\", \"", columns);

            var valuePlaceholders = string.Join(", ", Enumerable.Range(1, columns.Count).Select(i => $"${i}"));

            await using var cmd = new NpgsqlCommand($"INSERT INTO \"{tableName}\" (\"{columnsNames}\") VALUES ({valuePlaceholders}) RETURNING \"Id\";", PostgresConnection.conn);

            for (int i = 0; i < values.Count; i++)
            {
                cmd.Parameters.Add(values[i]);
            }

            var res = await cmd.ExecuteScalarAsync();

            if (res != null)
            {
                return ResultT<Guid>.Success((Guid)res);
            }

            return ResultT<Guid>.Failure(Error.Failure("=", ""));
        }

        public async Task<Result> UpdateAsync(Guid id, T obj)
        {
            string tableName = GetTableName();
            var (columns, values) = obj.GetInsertSQLObject();

            var updatePlaceholderString = "";
            for (int i = 0; i < columns.Count; i++)
            {
                updatePlaceholderString += $"\"{columns[i]}\" = ${i + 1}";
                if (i + 1 != columns.Count)
                {
                    updatePlaceholderString += ", ";
                }
            }

            await using var cmd = new NpgsqlCommand($"UPDATE \"{tableName}\" SET {updatePlaceholderString} WHERE \"Id\" = (${columns.Count + 1});", PostgresConnection.conn);

            for (int i = 0; i < values.Count; i++)
            {
                cmd.Parameters.Add(values[i]);
            }
            cmd.Parameters.Add(new NpgsqlParameter { Value = id, NpgsqlDbType = NpgsqlDbType.Uuid });

            var res = await cmd.ExecuteScalarAsync();

            if (res != null)
            {
                return Result.Success();
            }

            return Result.Failure(Error.Failure("=", ""));
        }

        public virtual async Task<Result> DeleteAsync(Guid id)
        {
            string tableName = GetTableName();

            return await DeleteFromTable(tableName, "Id", id);
        }

        internal async Task<Result> DeleteFromTable(string tableName, string property, Guid id)
        {
            await using var cmd = new NpgsqlCommand($"DELETE FROM \"{tableName}\" WHERE \"{property}\" = ($1)", PostgresConnection.conn)
            {
                Parameters =
                {
                    new() { Value = id, NpgsqlDbType = NpgsqlDbType.Uuid},
                }
            };

            if (cmd.ExecuteNonQuery() == -1)
            {
                return Result.Failure(Error.NotFound("404", $"Item on table {tableName} with given {property} not found"));
            }

            return Result.Success();
        }
    }
}