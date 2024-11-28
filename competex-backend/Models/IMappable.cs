using Npgsql;

public interface IMappable<T> where T : class
{
    public static abstract T Map(NpgsqlDataReader reader);
}