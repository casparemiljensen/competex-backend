using Microsoft.Extensions.Hosting;
using Npgsql;
using NpgsqlTypes;
using System.Numerics;

namespace competex_backend.Models
{
    public class ClubMembership : Identifiable, IMappable<ClubMembership>
    {
        public Guid ClubId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime JoinDate { get; set; }
        public ClubMemberRole? Role { get; set; } = ClubMemberRole.Standard;

        public static Task<ClubMembership> Map(NpgsqlDataReader reader)
        {
            return Task.FromResult(
                new ClubMembership
                {
                    Id = reader.GetGuid(0),
                    ClubId = reader.GetGuid(1),
                    MemberId = reader.GetGuid(2),
                    JoinDate = reader.GetDateTime(3),
                    Role = (ClubMemberRole)reader.GetInt16(4),
                }
            );
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = ClubId, NpgsqlDbType = NpgsqlDbType.Uuid},
                new NpgsqlParameter { Value = MemberId, NpgsqlDbType = NpgsqlDbType.Uuid},
                new NpgsqlParameter { Value = JoinDate, NpgsqlDbType = NpgsqlDbType.Timestamp},
                new NpgsqlParameter { Value = (short)Role!, NpgsqlDbType = NpgsqlDbType.Smallint}
            };

            var dbColumnNames = new List<string>
            {
                "ClubId",
                "MemberId",
                "JoinDate",
                "Role",
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}
