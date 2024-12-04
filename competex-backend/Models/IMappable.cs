using Npgsql;

public interface IMappable<T> where T : class
{
    public static abstract Task<T> Map(NpgsqlDataReader reader);
    public abstract Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject();
}