
using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models
{
    public class SportType : Identifiable, IMappable<SportType>
    {
        public string Name { get; set; } = string.Empty;
        public List<string> EventAttributes { get; set; } = new List<string>();
        // TODO: Investigate if need need these properties
        //public required IEnumerable<Club> Clubs { get; set; }
        //public required IEnumerable<Admin> Admins { get; set; }
        //public IEnumerable<Event>? Events { get; set; }
        //public required IEnumerable<CompetitionType> CompetitionTypes { get; set; }
        public EntityType EntityType { get; set; }

        public static async Task<SportType> Map(NpgsqlDataReader reader)
        {
            return new SportType()
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                EventAttributes = (await PostgresConnection.GetAnyList<data_CompetitionType_CompetitionAttributes>(
                    "data_SportType_EventAttributes", "SportTypeId", reader.GetGuid(0)))
                    .Select(x => x.CompetitionAttribute).ToList(),
                EntityType = EnumMapper.MapEnumValueTo<EntityType>(reader.GetInt16(3)).GetValueOrDefault()
            };
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = (short)EntityType, NpgsqlDbType = NpgsqlDbType.Smallint },
            };

            var dbColumnNames = new List<string>
            {
                "Name",
                "EntityType"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
