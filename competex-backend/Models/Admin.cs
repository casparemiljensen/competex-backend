
using competex_backend.DAL.Repositories.PostgressDataAccess;
using Npgsql;
using NpgsqlTypes;
using System.Xml.Linq;

namespace competex_backend.Models
{
    public class Admin : Member, IMappable<Admin>
    {
        public required List<Guid> SportTypeIds { get; set; }

        public Admin() : base()
        {

        }

        static async Task<Admin> IMappable<Admin>.Map(NpgsqlDataReader reader)
        {
            return new Admin
            {
                Id = reader.GetGuid(0),
                SportTypeIds = await PostgresConnection.GetGuidsByPropertyId(reader.GetGuid(0), "AdminSportType", "AdminId", "SportTypeId"),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Birthday = reader.GetDateTime(3),
                Email = reader.GetString(4),
                Phone = reader.GetString(5),
                Permissions = (Permissions)reader.GetInt16(6)
            };
        }

        new public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = FirstName, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = LastName, NpgsqlDbType = NpgsqlDbType.Text},
                new NpgsqlParameter { Value = Birthday, NpgsqlDbType = NpgsqlDbType.Timestamp},
                new NpgsqlParameter { Value = Email, NpgsqlDbType = NpgsqlDbType.Text},
                new NpgsqlParameter { Value = Phone, NpgsqlDbType = NpgsqlDbType.Text},
                new NpgsqlParameter { Value = (short)Permissions, NpgsqlDbType = NpgsqlDbType.Smallint},
            };

            var dbColumnNames = new List<string>
            {
                "FirstName",
                "LastName",
                "Birthday",
                "Email",
                "Phone",
                "Permissions"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
