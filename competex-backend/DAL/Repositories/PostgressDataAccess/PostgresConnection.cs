using competex_backend.Models;
using Npgsql;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

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

        private static string BuildConnectionString()
        {
            return $"Host={Config.Host};Username={Config.Username};Password={Config.Password};Database={Config.Database};Port={Config.Port}";
        }
    }
}
