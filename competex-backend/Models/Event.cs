
using competex_backend.DAL.Repositories.PostgressDataAccess;
using Npgsql;
using NpgsqlTypes;
using System.Xml.Linq;

namespace competex_backend.Models
{
    public class Event : Identifiable, IMappable<Event>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? LocationId { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public required Guid Organizer { get; set; } // ClubId
        public required Guid SportTypeId { get; set; }
        public List<Guid> CompetitionIds { get; set; } = [];
        public int EntryFee { get; set; } = 0;

        public static Task<Event> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(new Event
            {
                Id = reader.GetGuid(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                StartDate = reader.GetDateTime(3),
                EndDate = reader.GetDateTime(4),
                LocationId = reader.IsDBNull(5) ? null : reader.GetGuid(5),
                RegistrationStartDate = reader.GetDateTime(6),
                RegistrationEndDate = reader.GetDateTime(7),
                Status = (Status)reader.GetInt16(8),
                Organizer = reader.GetGuid(9),
                SportTypeId = reader.GetGuid(10),
                EntryFee = reader.GetInt32(11),
            });
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Title, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = Description, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = StartDate, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = EndDate, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = LocationId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = RegistrationStartDate, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = RegistrationEndDate, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = (short)Status, NpgsqlDbType = NpgsqlDbType.Smallint },
                new NpgsqlParameter { Value = Organizer, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = SportTypeId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = EntryFee, NpgsqlDbType = NpgsqlDbType.Integer },
            };

            var dbColumnNames = new List<string>
            {
                "Title",
                "Description",
                "StartDate",
                "EndDate",
                "Location",
                "RegistrationStartDate",
                "RegistrationEndDate",
                "Status",
                "Organizer",
                "SportType",
                "EntryFee"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
