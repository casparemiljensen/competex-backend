using System.Data;
using competex_backend.Models;
using Npgsql;
using NpgsqlTypes;

class data_CompetitionType_CompetitionAttributes : IMappable<data_CompetitionType_CompetitionAttributes>
{
    public Guid CompetitionTypeId { get; set; }
    public string CompetitionAttribute { get; set; }
    public static Task<data_CompetitionType_CompetitionAttributes> Map(NpgsqlDataReader reader)
    {
        return Task.FromResult(
            new data_CompetitionType_CompetitionAttributes()
            {
                CompetitionTypeId = reader.GetGuid(0),
                CompetitionAttribute = reader.GetString(1)
            });
    }

    public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
    {
        var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = CompetitionTypeId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = CompetitionAttribute, NpgsqlDbType = NpgsqlDbType.Text },
            };

        var dbColumnNames = new List<string>
            {
                "CompetitionTypeId",
                "CompetitionAttribute"
            };

        return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
    }
}