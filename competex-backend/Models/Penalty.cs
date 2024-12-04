using System.Data;
using competex_backend.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models
{
    public class Penalty : Identifiable, IMappable<Penalty>
    {
        public PenaltyType PenaltyType { get; set; }
        public object? PenaltyValue { get; set; }

        public static Task<Penalty> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new Penalty
                {
                    Id = reader.GetGuid(0),
                    PenaltyValue = reader.GetString(1) as object,
                    PenaltyType = EnumMapper.MapEnumValueTo<PenaltyType>(reader.GetInt16(2)).GetValueOrDefault(),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = PenaltyValue, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = (short)PenaltyType, NpgsqlDbType = NpgsqlDbType.Smallint },
            };

            var dbColumnNames = new List<string>
            {
                "PenaltyValue",
                "PenaltyType"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
