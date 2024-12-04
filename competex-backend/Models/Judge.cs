using competex_backend.Models;
using Npgsql;
using NpgsqlTypes;
using System.Xml.Linq;

namespace competex_backend.Models
{
    public class Judge : Identifiable, IMappable<Judge>
    {
        public JudgeType JudgeType { get; set; }
        public Guid MemberId { get; set; }  
        public string Description { get; set; }

        public static Task<Judge> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new Judge
                {
                    Id = reader.GetGuid(3),
                    MemberId = reader.GetGuid(0),
                    Description = reader.GetString(1),
                    JudgeType = (JudgeType)reader.GetInt16(2),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = MemberId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = Description, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = (short)JudgeType, NpgsqlDbType = NpgsqlDbType.Smallint },
            };

            var dbColumnNames = new List<string>
            {
                "MemberId",
                "Description",
                "JudgeType"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
