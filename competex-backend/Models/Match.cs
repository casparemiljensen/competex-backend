using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Helpers;
using competex_backend.Models;
using Npgsql;
using NpgsqlTypes;
using System.Numerics;

namespace competex_backend.Models
{
    public class Match : Identifiable, IMappable<Match>
    {
        public Guid RoundId { get; set; }
        public List<Guid>? ParticipantIds { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.Pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? FieldId { get; set; }
        public Guid? JudgeId { get; set; }
        public List<Guid>? ScoreIds { get; set; } = [];

        public static async Task<Match> Map(NpgsqlDataReader reader)
        {
            var participantIdsTask = PostgresConnection.GetGuidsByPropertyId(reader.GetGuid(0), "MatchParticipants", "MatchId", "ParticipantId");
            var scoreIdsTask = PostgresConnection.GetGuidsByPropertyId(reader.GetGuid(0), "MatchScores", "MatchId", "ScoreId");
            await Task.WhenAll(participantIdsTask, scoreIdsTask);
            return new Match
            {
                Id = reader.GetGuid(0),
                RoundId = reader.GetGuid(1),
                Status = EnumMapper.MapEnumValueTo<MatchStatus>(reader.GetInt16(2)).GetValueOrDefault(),
                StartTime = reader.GetDateTime(3),
                EndTime = reader.GetDateTime(4),
                FieldId = reader.GetGuid(5),
                JudgeId = reader.GetGuid(6),
                ParticipantIds = participantIdsTask.Result,
                ScoreIds = scoreIdsTask.Result, 
            };
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = RoundId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = (short)Status, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = StartTime, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = EndTime, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = FieldId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = JudgeId, NpgsqlDbType = NpgsqlDbType.Uuid }
            };

                var dbColumnNames = new List<string>
            {
                "RoundId",
                "Status",
                "StartTime",
                "EndTime",
                "FieldId",
                "JudgeId"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
