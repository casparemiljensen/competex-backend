using Npgsql;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public class Connection
    {
        private readonly NpgsqlConnection connection;
        private bool isBusy = false;

        public Connection(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public NpgsqlConnection GetConnection()
        {
            isBusy = true;
            return connection;
        }

        public void EndQuery()
        {
            isBusy = true;
        }

        public bool IsReady()
        {
            return !isBusy;
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
