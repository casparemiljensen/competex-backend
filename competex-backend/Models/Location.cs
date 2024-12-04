
using Npgsql;

namespace competex_backend.Models
{
    public class Location : Identifiable, IMappable<Location>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public static Task<Location> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new Location
                {
                    Id = reader.GetGuid(0),
                    Address = reader.GetString(1),
                    Zip = reader.GetString(2),
                    City = reader.GetString(3),
                    Country = reader.GetString(4),
                    Name = reader.GetString(5),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            throw new NotImplementedException();
        }
    }
}
