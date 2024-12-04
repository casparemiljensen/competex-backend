using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models
{
    public class ScoringSystem : Identifiable, IMappable<ScoringSystem>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ScoreType ScoreType { get; set; }
        public string ScoringRules { get; set; } = string.Empty;
        public List<Penalty> Penalties { get; set; } = [];
        public Func<ScoreType, int, int>? EvaluationMethod { get; set; }

        public static async Task<ScoringSystem> Map(NpgsqlDataReader reader)
        {
            return new ScoringSystem
            {
                Id = reader.GetGuid(0),
                Description = reader.GetString(1),
                ScoreType = EnumMapper.MapEnumValueTo<ScoreType>(reader.GetInt16(2)).GetValueOrDefault(),
                ScoringRules = reader.GetString(3),
                Penalties = (await PostgresConnection.GetManyManyList<Penalty>("ScoringSystemPenalties", "ScoringSystemId", "Penalty", "PenaltyId", reader.GetGuid(0))).ToList(),
                Name = reader.GetString(5),
            };
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Description, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = (short)ScoreType, NpgsqlDbType = NpgsqlDbType.Smallint },
                new NpgsqlParameter { Value = ScoringRules, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text },
            };

            var dbColumnNames = new List<string>
            {
                "Description",
                "ScoreType",
                "ScoringRules",
                "Name"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
