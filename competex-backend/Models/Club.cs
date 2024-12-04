using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models
{
    public class Club : Identifiable, IMappable<Club>
    {
        public string Name { get; set; } = string.Empty;
        public string AssociatedSport { get; set; } = string.Empty;

        public static Task<Club> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new Club
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    AssociatedSport = reader.GetString(2),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text},
                new NpgsqlParameter {Value = AssociatedSport, NpgsqlDbType = NpgsqlDbType.Text},
            };

            var dbColumnNames = new List<string>
            {
                "Name",
                "AssociatedSport"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
        //ved ikke om denne giver mening. Skal måske ændres til AssociatedSports hvis det er, da klubber kan have flere sportsgrene i mine øjne - Ilum
    }
}
