

using Common.ResultPattern;
using competex_backend.Common.ErrorHandling;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public class PostgresGenericRepository<T> : IGenericRepository<T> where T : class, IIdentifiable, IMappable<T>
    {
        public async Task<ResultT<Tuple<int, IEnumerable<T>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            string tableName = GetTableName();

            await using var command = new NpgsqlCommand($"SELECT * FROM \"{tableName}\";", PostgresConnection.conn);

            await using var reader = await command.ExecuteReaderAsync();

            return PaginationHelper.PaginationWrapper<T>(await IterateOverReader(reader), pageSize, pageNumber);
        }

        private static string GetTableName()
        {
            switch (Activator.CreateInstance<T>())
            {
                case Member:
                    return "Member";
                default:
                    throw new ApiException(400, "Bad request: Invalid type requested");
            }
        }

        private async static Task<List<T>> IterateOverReader(NpgsqlDataReader reader)
        {
            List<T> items = [];
            //do
            while (await reader.ReadAsync())
            {
                items.Add(T.Map(reader));
            }
            return items;
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

            if(!await reader.ReadAsync())
            {
                return ResultT<T>.Failure(Error.NotFound("400", "Item with given id not found"));
            }

            return ResultT<T>.Success(T.Map(reader));
        }

        public Task<ResultT<Tuple<int, IEnumerable<T>>>> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters)
        {
            throw new NotImplementedException();
        }

        public Task<ResultT<Guid>> InsertAsync(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAsync(Guid id, T obj)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            string tableName = GetTableName();

            await using var cmd = new NpgsqlCommand($"SELECT * FROM \"{tableName}\" WHERE \"Id\" = ($1)", PostgresConnection.conn)
            {
                Parameters =
                {
                    new() { Value = id, NpgsqlDbType = NpgsqlDbType.Uuid},
                }
            };



            return Result.Success(); // TODO: Fix
        }
    }
}
