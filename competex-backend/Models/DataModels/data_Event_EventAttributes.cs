using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models.DataModels
{
    public class data_Event_EventAttributes : IMappable<data_Event_EventAttributes>
    {
        public Guid EventId { get; set; }
        public required string EventAttribute { get; set; }
        public static Task<data_Event_EventAttributes> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new data_Event_EventAttributes()
                {
                    EventId = reader.GetGuid(0),
                    EventAttribute = reader.GetString(1)
                });
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = EventId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = EventAttribute, NpgsqlDbType = NpgsqlDbType.Text },
            };

            var dbColumnNames = new List<string>
            {
                "EventId",
                "EventAttribute"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
