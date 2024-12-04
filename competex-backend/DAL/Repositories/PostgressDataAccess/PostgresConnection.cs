using competex_backend.Models;
using Npgsql;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Net.Http.Headers;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public static class PostgresConnection
    {
        public static NpgsqlConnection conn;
        private static class Config
        {
            public const string Host = "localhost:15432";
            public const int Port = 5432;
            public const string Database = "competexdb";
            public const string Username = "postgres";
            public const string Password = "TestPassword";
            public const bool UseSsl = false;
        }

        public static async void Connect()
        {
            Console.WriteLine(BuildConnectionString());
            await using var dataSource = NpgsqlDataSource.Create(BuildConnectionString());
            conn = await dataSource.OpenConnectionAsync();
        }
        public static bool IsValidSQLString(string name)
        {
            // Basic validation: check for allowed characters
            return name.All(c => char.IsLetterOrDigit(c) || c == '_');
        }
        public static async Task<List<T>> GetAnyList<T>(string tableName, string property, object searchValue) where T : class, IMappable<T>
        {

            List<T> output = [];
            if (!IsValidSQLString(tableName) || !IsValidSQLString(property))
            {
                return output;
            }

            await using var command = new NpgsqlCommand($"SELECT * FROM \"{tableName}\" WHERE \"{property}\" = ($1);", PostgresConnection.conn);

            List<NpgsqlParameter> paramList = [];
            paramList.AddTypeCorrectFilter(searchValue);
            command.Parameters.Add(paramList[0]);

            await using var reader = await command.ExecuteReaderAsync();

            return await SearchHelper.IterateOverReader<T>(reader);
        }
        public static async Task<List<outT>> GetManyManyList<outT>(
            string relationTable,
            string relationJoinProperty,
            string valueTable,
            string relationTableValueProperty,
            Guid parentGuid) where outT : class, IIdentifiable, IMappable<outT>
        {

            List<outT> output = [];
            if (!IsValidSQLString(relationTable)
                || !IsValidSQLString(valueTable)
                || !IsValidSQLString(relationTableValueProperty)
                || !IsValidSQLString(relationJoinProperty))
            {
                return output;
            }

            await using var command = new NpgsqlCommand($"SELECT mem.* " +
            "FROM \"{relationTable}\" rel " +
                " WHERE \"{relationJoinProperty}\" = ($1)" +
            "JOIN \"{valueTable}\" vt " +
                "ON rel.\"{relationTableValueProperty}\" = vt.\"Id\"",
            conn);

            List<NpgsqlParameter> paramList = [];
            paramList.AddTypeCorrectFilter(parentGuid);
            command.Parameters.Add(paramList[0]);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                output.Add(await outT.Map(reader));
            }
            return output;
        }

        private static string BuildConnectionString()
        {
            return $"Host={Config.Host};Username={Config.Username};Password={Config.Password};Database={Config.Database};Port={Config.Port}";
        }
    }
}
