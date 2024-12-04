using competex_backend.Models;
using Microsoft.AspNetCore.SignalR;
using Npgsql;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Data;
using System.Net.Http.Headers;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public static class PostgresConnection
    {
        private static List<Connection> connections = [];
        private static class Config
        {
            public const string Host = "localhost:15432";
            public const int Port = 5432;
            public const string Database = "competexdb";
            public const string Username = "postgres";
            public const string Password = "TestPassword";
            public const bool UseSsl = false;
        }

        private static async Task Connect()
        {
            Console.WriteLine(BuildConnectionString());
            await using var dataSource = NpgsqlDataSource.Create(BuildConnectionString());
            var connection = new Connection(await dataSource.OpenConnectionAsync());
            connections.Add(connection);
        }
        public static bool IsValidSQLString(string name)
        {
            // Basic validation: check for allowed characters
            return name.All(c => char.IsLetterOrDigit(c) || c == '_');
        }

        internal static string GetTableName<T>()
        {
            switch (Activator.CreateInstance<T>())
            {
                case Member:
                    return "Member";
                case Match:
                    return "Match";
                default:
                    Console.WriteLine("Unknown type: " + typeof(T).Name);
                    return typeof(T).Name;
            }
        }

        public static async Task<ResultT<Guid>> Insert<T>(T obj) where T : class, IMappable<T>
        {
            string tableName = GetTableName<T>();
            var internalGuid = Guid.Empty;
            var (columns, values) = obj.GetInsertSQLObject();

            var columnsNames = String.Join("\", \"", columns);

            var valuePlaceholders = string.Join(", ", Enumerable.Range(1, columns.Count).Select(i => $"${i}"));

            var connection = await GetReadyConnection();

            await using var cmd = new NpgsqlCommand($"INSERT INTO \"{tableName}\" (\"{columnsNames}\") VALUES ({valuePlaceholders}) RETURNING \"Id\";", connection.GetConnection());

            for (int i = 0; i < values.Count; i++)
            {
                cmd.Parameters.Add(values[i]);
            }

            var res = await cmd.ExecuteScalarAsync();

            connection.EndQuery();

            if (res != null)
            {
                return ResultT<Guid>.Success((Guid)res);
            }

            return ResultT<Guid>.Failure(Error.Failure("=", ""));
        }
        public static async Task<List<T>> GetAnyList<T>(string tableName, string property, object searchValue) where T : class, IMappable<T>
        {

            List<T> output = [];
            if (!IsValidSQLString(tableName) || !IsValidSQLString(property))
            {
                return output;
            }
            Connection connection = await GetReadyConnection();

            await using var command = new NpgsqlCommand($"SELECT * FROM \"{tableName}\" WHERE \"{property}\" = ($1);", connection.GetConnection());

            List<NpgsqlParameter> paramList = [];
            paramList.AddTypeCorrectFilter(searchValue);
            command.Parameters.Add(paramList[0]);

            await using var reader = await command.ExecuteReaderAsync();

            return await SearchHelper.IterateOverReader<T>(reader, connection);
        }

        public static async Task<List<Guid>> GetGuidsByPropertyId(Guid id, string tableName, string propertyName, string selectProperty)
        {
            List<Guid> output = [];
            if (!IsValidSQLString(tableName)
                || !IsValidSQLString(propertyName)
                || !IsValidSQLString(selectProperty))
            {
                return output;
            }

            Connection connection = await GetReadyConnection();

            await using var command = new NpgsqlCommand($"SELECT \"{propertyName}\"" +
            $"FROM \"{tableName}\"" +
                $" WHERE \"{propertyName}\" = ($1)",
            connection.GetConnection());

            List<NpgsqlParameter> paramList = [];
            paramList.AddTypeCorrectFilter(id);
            command.Parameters.Add(paramList[0]);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                output.Add(reader.GetGuid(0));
            }
            connection.EndQuery();
            return output;
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

            Connection connection = await GetReadyConnection();
            await using var command = new NpgsqlCommand($"SELECT mem.* " +
            $"FROM \"{relationTable}\" rel " +
                $"WHERE \"{relationJoinProperty}\" = ($1) " +
            $"JOIN \"{valueTable}\" vt " +
                $"ON rel.\"{relationTableValueProperty}\" = vt.\"Id\"",
            connection.GetConnection());

            List<NpgsqlParameter> paramList = [];
            paramList.AddTypeCorrectFilter(parentGuid);
            command.Parameters.Add(paramList[0]);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                output.Add(await outT.Map(reader));
            }
            connection.EndQuery();
            return output;
        }
        
        public async static Task<Connection> GetReadyConnection()
        {
            for(int i = 0; i < connections.Count; i++)
            {
                if (connections[i].IsReady())
                {
                    return connections[i];
                }
            }
            Console.WriteLine("Creating new connection");
            await Connect();
            var lastConnection = connections.Last();
            if (!lastConnection.IsReady())
            {
                throw new InvalidOperationException("The newly created connection is not ready.");
            }

            return lastConnection;
        }

        private static string BuildConnectionString()
        {
            return $"Host={Config.Host};Username={Config.Username};Password={Config.Password};Database={Config.Database};Port={Config.Port}";
        }
    }
}
