using System;
using competex_backend.API.Controllers;
using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models;

public class Round : Identifiable, IMappable<Round>
{
    public required string Name { get; set; }
    public uint SequenceNumber { get; set; }
    public RoundType RoundType { get; set; }
    public Guid CompetitionId { get; init; }
    public RoundStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    //public List<Match> Matches { get; set; }
    //public List<Participant> Participants { get; set; }

    public static async Task<Round> Map(NpgsqlDataReader reader)
    {
        return new Round
        {
            Id = reader.GetGuid(0),
            SequenceNumber = (uint)reader.GetInt32(1),
            RoundType = EnumMapper.MapEnumValueTo<RoundType>(reader.GetInt16(2)).GetValueOrDefault(),
            CompetitionId = reader.GetGuid(3),
            Status = EnumMapper.MapEnumValueTo<RoundStatus>(reader.GetInt16(4)).GetValueOrDefault(),
            StartTime = reader.GetDateTime(5),
            EndTime = reader.GetDateTime(6),
            Name = reader.GetString(7),
            //Matches = (await PostgresConnection.GetManyManyList<Match>("RoundMatches", "RoundId", "Match", "MatchId", reader.GetGuid(0))).ToList(),
            //Participants = (await PostgresConnection.GetManyManyList<Participant>("RoundParticipants", "RoundId", "Participant", "ParticipantId", reader.GetGuid(0))).ToList(),
        };
    }

    public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
    {
        var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = (int)SequenceNumber, NpgsqlDbType = NpgsqlDbType.Integer },
                new NpgsqlParameter { Value = (short)RoundType, NpgsqlDbType = NpgsqlDbType.Smallint },
                new NpgsqlParameter { Value = CompetitionId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = (short)Status, NpgsqlDbType = NpgsqlDbType.Smallint },
                new NpgsqlParameter { Value = StartTime, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = EndTime, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text },
            };

        var dbColumnNames = new List<string>
            {
                "SequenceNumber",
                "RoundType",
                "CompetitionId",
                "Status",
                "StartTime",
                "EndTime",
                "Name",
            };

        return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
    }

    //public IEnumerable<Guid> MatchIds { get; set; } = new List<Guid>();


    public override string ToString()
    {
        return $"Id: {Id}\nName: {Name}\nSequenceNumber: {SequenceNumber}";
    }
}