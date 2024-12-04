
using AutoMapper.Execution;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace competex_backend.Models
{
    public class Competition : Identifiable, IMappable<Competition>
    {
        public required Guid CompetitionTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Level Level { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public required int RegistrationPrice { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }

        public static Task<Competition> Map(NpgsqlDataReader reader)
        {
            Console.WriteLine(reader.GetGuid(1));
            return Task.FromResult(
                new Competition
                {
                    Id = reader.GetGuid(0),
                    CompetitionTypeId = reader.GetGuid(1),
                    StartDate = reader.GetDateTime(2),
                    EndDate = reader.GetDateTime(3),
                    Level = (Level)reader.GetInt16(4),
                    Status = (Status)reader.GetInt16(5),
                    MinParticipants = reader.GetInt32(6),
                    MaxParticipants = reader.GetInt32(7),
                    RegistrationPrice = reader.GetInt32(8),
                    EventId = reader.GetGuid(9),
                    Name = reader.GetString(10),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = CompetitionTypeId, NpgsqlDbType = NpgsqlDbType.Uuid},
                new NpgsqlParameter { Value = StartDate, NpgsqlDbType = NpgsqlDbType.Timestamp},
                new NpgsqlParameter { Value = EndDate, NpgsqlDbType = NpgsqlDbType.Timestamp},
                new NpgsqlParameter { Value = (short)Level, NpgsqlDbType = NpgsqlDbType.Smallint},
                new NpgsqlParameter { Value = (short)Status, NpgsqlDbType = NpgsqlDbType.Smallint},
                new NpgsqlParameter { Value = MinParticipants, NpgsqlDbType = NpgsqlDbType.Integer},
                new NpgsqlParameter { Value = MaxParticipants, NpgsqlDbType = NpgsqlDbType.Integer},
                new NpgsqlParameter { Value = RegistrationPrice, NpgsqlDbType = NpgsqlDbType.Integer},
                new NpgsqlParameter { Value = EventId, NpgsqlDbType = NpgsqlDbType.Uuid},
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Uuid},
            };

            var dbColumnNames = new List<string>
            {
                "CompetitionType",
                "StartDate",
                "EndDate",
                "Level",
                "MinParticipants",
                "MaxParticipants",
                "RegistrationPrice",
                "EventId",
                "Name"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
