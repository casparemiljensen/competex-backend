using System.ComponentModel.DataAnnotations;
using competex_backend.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models
{
    public class Entity : Identifiable, IMappable<Entity>
    {
        public EntityType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public Level Level { get; set; }
        public Guid OwnerId { get; set; }

        public static Task<Entity> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new Entity
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Birthdate = reader.GetDateTime(2),
                    Level = EnumMapper.MapEnumValueTo<Level>(reader.GetInt16(3)).GetValueOrDefault(),
                    OwnerId = reader.GetGuid(4),
                    Type = EnumMapper.MapEnumValueTo<EntityType>(reader.GetInt16(5)).GetValueOrDefault(),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = Birthdate, NpgsqlDbType = NpgsqlDbType.Timestamp },
                new NpgsqlParameter { Value = (short)Level, NpgsqlDbType = NpgsqlDbType.Smallint },
                new NpgsqlParameter { Value = OwnerId, NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter { Value = (short)Type, NpgsqlDbType = NpgsqlDbType.Smallint },
            };

            var dbColumnNames = new List<string>
            {
                "Name",
                "Birthday",
                "Level",
                "Owner",
                "Type"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}