

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

            await using var command = new NpgsqlCommand($"SELECT * FROM {tableName};", PostgresConnection.conn);

            await using var reader = await command.ExecuteReaderAsync();

            return ((await CallAccordingToType(reader, pageSize, pageNumber, false)) as ResultT<Tuple<int, IEnumerable<T>>>)!;

        }

        private static async Task<object> CallAccordingToType(NpgsqlDataReader reader, int? pageSize, int? pageNumber, bool isSingle = true)
        {
            await reader.ReadAsync();
            switch (Activator.CreateInstance<T>())
            {
                case Member:
                    return isSingle ? T.Map(reader) : PaginationHelper.PaginationWrapper(await IterateOverReader(reader), pageSize, pageNumber);
            }
            throw new ApiException(400, "Invalid request, Type not found");
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
            do
            {
                items.Add(T.Map(reader));
            }
            while (await reader.ReadAsync());
            return items;
        }

        public async Task<ResultT<T>> GetByIdAsync(Guid id)
        {
            string tableName = GetTableName();

            //using var cmd = new NpgsqlCommand($"SELECT * FROM member WHERE id = '58a01cc0-1a49-455b-998c-1500b3db0dca'", PostgresConnection.conn)
            await using var cmd = new NpgsqlCommand($"SELECT * FROM {tableName} WHERE \"id\" = ($1)", PostgresConnection.conn)
            {
                Parameters =
                {
                    new() { Value = id, NpgsqlDbType = NpgsqlDbType.Uuid},
                }
            };

            await using var reader = await cmd.ExecuteReaderAsync();

            return ResultT<T>.Success(((await CallAccordingToType(reader, null, null, true)) as T)!);
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

        public Task<Result> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
