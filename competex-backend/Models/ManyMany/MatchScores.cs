using Npgsql;
using NpgsqlTypes;
using System.Xml.Linq;

namespace competex_backend.Models.ManyMany
{
    public class MatchScores : IIdentifiable, IMappable<MatchScores>
    {
        public Guid MatchId { get; set; }
        public Guid ScoreId { get; set; }
        public Guid Id { get; set; }

        public static Task<MatchScores> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new MatchScores
                {
                    MatchId = reader.GetGuid(0),
                    ScoreId = reader.GetGuid(1),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = MatchId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = ScoreId, NpgsqlDbType = NpgsqlDbType.Uuid },
            };

            var dbColumnNames = new List<string>
            {
                "MatchId",
                "ScoreId",
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
