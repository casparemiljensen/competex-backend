using Npgsql;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public class Connection
    {
        private readonly NpgsqlConnection connectionA;
        private bool isBusy = false;

        public Connection(NpgsqlConnection _connection)
        {
            connectionA = _connection;
        }

        public NpgsqlConnection GetConnection()
        {
            isBusy = true;
            return connectionA;
        }

        public void EndQuery()
        {
            isBusy = false;
        }

        public bool IsReady()
        {
            return !isBusy && connectionA.State == System.Data.ConnectionState.Open;
        }

        public void Dispose()
        {
            connectionA.Dispose();
        }
    }
}
