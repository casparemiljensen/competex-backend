﻿using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Helpers;
using competex_backend.Models;
using Npgsql;

namespace competex_backend.Models
{
    public class Match : Identifiable, IMappable<Match>
    {
        public Guid RoundId { get; set; }
        public List<Participant>? Participants { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.Pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? FieldId { get; set; }
        public Guid? JudgeId { get; set; }
        public List<Score>? Scores { get; set; } = [];

        public static async Task<Match> Map(NpgsqlDataReader reader)
        {
            return new Match
            {
                Id = reader.GetGuid(0),
                RoundId = reader.GetGuid(1),
                Status = EnumMapper.MapEnumValueTo<MatchStatus>(reader.GetInt16(2)).GetValueOrDefault(),
                StartTime = reader.GetDateTime(3),
                EndTime = reader.GetDateTime(4),
                FieldId = reader.GetGuid(5),
                JudgeId = reader.GetGuid(6),
                Participants = (await PostgresConnection.GetManyManyList<Participant>("MatchParticipants", "MatchId", "Participant", "ParticipantId", reader.GetGuid(0))).ToList(),
                Scores = (await PostgresConnection.GetManyManyList<Score>("MatchScores", "MatchId", "Score", "ScoreId", reader.GetGuid(0))).ToList(),
            };
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            throw new NotImplementedException();
        }
    }
}
