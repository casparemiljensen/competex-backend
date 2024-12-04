using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models.DataModels
{
    public class data_SportType_EventAttributes
    {
        public Guid SportTypeId { get; set; }
        public required string EventAttribute { get; set; }
        public static Task<data_SportType_EventAttributes> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new data_SportType_EventAttributes()
                {
                    SportTypeId = reader.GetGuid(0),
                    EventAttribute = reader.GetString(1)
                });
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = SportTypeId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = EventAttribute, NpgsqlDbType = NpgsqlDbType.Text },
            };

            var dbColumnNames = new List<string>
            {
                "SportTypeId",
                "EventAttribute"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
