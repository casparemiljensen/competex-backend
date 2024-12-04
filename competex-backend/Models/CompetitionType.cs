
using competex_backend.DAL.Repositories.PostgressDataAccess;
using Npgsql;

namespace competex_backend.Models
{
    public class CompetitionType : Identifiable, IMappable<CompetitionType>
    {
        public string Name { get; set; } = string.Empty;
        public List<string> CompetitionAttributes { get; set; } = new List<string>();
        public ScoreType ScoreType { get; set; }
        public ScoreMethod ScoreMethod { get; set; }

        public static async Task<CompetitionType> Map(NpgsqlDataReader reader)
        {
            return new CompetitionType
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                CompetitionAttributes = (await PostgresConnection.GetAnyList<data_CompetitionType_CompetitionAttributes>(
                    "data_CompetitionType_CompetitionAttributes", "CompetitionTypeId", reader.GetGuid(0)))
                    .Select(x => x.CompetitionAttribute).ToList(),
                ScoreType = (ScoreType)reader.GetInt16(2),
                ScoreMethod = (ScoreMethod)reader.GetInt16(3),
            };
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            throw new NotImplementedException();
        }
    }
}
