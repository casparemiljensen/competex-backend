using System.Text.Json;
using competex_backend.DAL.Repositories.PostgressDataAccess;
using Npgsql;
using NpgsqlTypes;

public static class SearchHelper
{
    public async static Task<List<T>> IterateOverReader<T>(NpgsqlDataReader reader, Connection connection) where T : class, IMappable<T>
    {
        List<T> items = [];
        //do
        while (await reader.ReadAsync())
        {
            items.Add(await T.Map(reader));
        }
        connection.EndQuery();
        return items;
    }
    public static void AddTypeCorrectFilter(this List<NpgsqlParameter> list, object filter)
    {
        DateTime time;
        Guid guid;
        short _short;
        int integer;
        double _double;

        var filterValue = filter;
        string stringElement;

        //Json Types
        if (filterValue is JsonElement jsonElement)
        {
            stringElement = jsonElement.ToString();

            if (Guid.TryParse(stringElement, out guid))
            {
                list.Add(new NpgsqlParameter() { Value = guid, NpgsqlDbType = NpgsqlDbType.Uuid });
                return;
            }
            if (DateTime.TryParse(stringElement, out time))
            {
                Console.WriteLine("DATETIME");
                list.Add(new NpgsqlParameter() { Value = time, NpgsqlDbType = NpgsqlDbType.Timestamp });
                return;
            }
            if (jsonElement.ValueKind == JsonValueKind.Number)
            {
                if (short.TryParse(stringElement, out _short))
                {
                    list.Add(new NpgsqlParameter() { Value = _short, NpgsqlDbType = NpgsqlDbType.Smallint });
                    return;
                }
                else if (int.TryParse(stringElement, out integer))
                {
                    list.Add(new NpgsqlParameter() { Value = integer, NpgsqlDbType = NpgsqlDbType.Integer });
                    return;
                }
                else if (double.TryParse(stringElement, out _double))
                {
                    list.Add(new NpgsqlParameter() { Value = _double, NpgsqlDbType = NpgsqlDbType.Double });
                    return;
                }
                else
                {
                    Console.WriteLine("Unknown number type used");
                    return;
                }
            }
            if (jsonElement.ValueKind == JsonValueKind.String)//Must after parsers
            {
                list.Add(new NpgsqlParameter() { Value = stringElement, NpgsqlDbType = NpgsqlDbType.Text });
                return;
            }
        }

        //Native types
        if (filterValue is Guid)
        {
            list.Add(new NpgsqlParameter() { Value = filterValue, NpgsqlDbType = NpgsqlDbType.Uuid });
        }
        else if (filterValue.GetType().IsAssignableTo(typeof(string)))
        {
            list.Add(new NpgsqlParameter() { Value = filterValue.ToString(), NpgsqlDbType = NpgsqlDbType.Text });
        }
        else if (DateTime.TryParse(filterValue.ToString(), out time))
        {
            list.Add(new NpgsqlParameter() { Value = time, NpgsqlDbType = NpgsqlDbType.TimestampTz });
        }
        else
        {
            Console.WriteLine("Unknown type: " + filterValue.GetType().Name + " value: " + filterValue);
        }
    }
}