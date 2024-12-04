using Npgsql;

namespace competex_backend.Models
{
    public class Field : Identifiable, IMappable<Field>
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public SurfaceType? Surface { get; set; } = SurfaceType.Unknown;

        public static Task<Field> Map(NpgsqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            throw new NotImplementedException();
        }
    }
}