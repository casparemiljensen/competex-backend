using competex_backend.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models
{
    public class Registration : Identifiable, IMappable<Registration>
    {
        public Guid ParticipantId { get; set; }
        public Guid CompetitionId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RegistrationStatus Status { get; set; }

        public static Task<Registration> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new Registration
                {
                    Id = reader.GetGuid(0),
                    ParticipantId = reader.GetGuid(1),
                    CompetitionId = reader.GetGuid(2),
                    RegistrationDate = reader.GetDateTime(3),
                    Status = EnumMapper.MapEnumValueTo<RegistrationStatus>(reader.GetInt16(4)).GetValueOrDefault(),
                });
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = ParticipantId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = CompetitionId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = RegistrationDate, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = (short)Status, NpgsqlDbType = NpgsqlDbType.Smallint },
            };

            var dbColumnNames = new List<string>
            {
                "ParticipantId",
                "CometitionId",
                "RegistrationDate",
                "Status"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
