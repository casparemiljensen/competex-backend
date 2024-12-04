using competex_backend.DAL.Repositories.PostgressDataAccess;
using competex_backend.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.Models
{
    public abstract class Participant : Identifiable, IMappable<Participant>
    {
        public string Name { get; set; }
        public ParticipantType ParticipantType { get; set; }

        public static async Task<Participant> Map(NpgsqlDataReader reader)
        {
            var type = EnumMapper.MapEnumValueTo<ParticipantType>(reader.GetInt16(3)).GetValueOrDefault();
            switch (type)
            {
                case ParticipantType.Single:
                    return await Single.Map(reader);
                case ParticipantType.Team:
                    return await Team.Map(reader);
                case ParticipantType.Ekvipage:
                    return await Ekvipage.Map(reader);
                default:
                    throw new Exception("Invalid ParticipantType");
            }
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            throw new NotImplementedException();
        }
    }

    public class Team : Participant, IMappable<Team>
    {
        public List<Member> Members { get; set; }
        public Team(string name, List<Member> members) : base()
        {
            Members = members;
        }

        new public static async Task<Team> Map(NpgsqlDataReader reader)
        {
            var type = EnumMapper.MapEnumValueTo<ParticipantType>(reader.GetInt16(3)).GetValueOrDefault();
            return new Team(
                    reader.GetString(2),
                    (await PostgresConnection.GetManyManyList<Member>("ParticipantMembers", "ParticipantId", "Member", "MemberId",
                        reader.GetGuid(0))))
            {
                Id = reader.GetGuid(0),
                ParticipantType = type,
            };
        }

        new public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = (short)ParticipantType.Team, NpgsqlDbType = NpgsqlDbType.Smallint },
            };

            var dbColumnNames = new List<string>
            {
                "Name",
                "ParticipantType"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }

    public class Single : Participant, IMappable<Single>
    {
        public Member Member { get; set; }
        public Single(string Name, Member member) : base()
        {
            Member = member;
        }

        new public static async Task<Single> Map(NpgsqlDataReader reader)
        {
            var type = EnumMapper.MapEnumValueTo<ParticipantType>(reader.GetInt16(3)).GetValueOrDefault();
            return new Single(
                    reader.GetString(2),
                    (await PostgresConnection.GetManyManyList<Member>("ParticipantMembers", "ParticipantId", "Member", "MemberId",
                        reader.GetGuid(0)))[0])
            {
                Id = reader.GetGuid(0),
                ParticipantType = type,
            };
        }

        new public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = (short)ParticipantType.Single, NpgsqlDbType = NpgsqlDbType.Smallint },
            };

            var dbColumnNames = new List<string>
            {
                "Name",
                "ParticipantType"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }

    public class Ekvipage : Participant, IMappable<Ekvipage>
    {
        public Member Member { get; set; }
        public Entity Entity { get; set; }
        public Ekvipage(string name, Member member, Entity entity) : base()
        {
            Member = member;
            Entity = entity;
        }

        new public static async Task<Ekvipage> Map(NpgsqlDataReader reader)
        {
            var type = EnumMapper.MapEnumValueTo<ParticipantType>(reader.GetInt16(3)).GetValueOrDefault();
            return new Ekvipage(
                    reader.GetString(2),
                    (await PostgresConnection.GetManyManyList<Member>("ParticipantMembers", "ParticipantId", "Member", "MemberId",
                        reader.GetGuid(0)))[0],
                    (await PostgresConnection.GetAnyList<Entity>("Entity", "Id", reader.GetGuid(0)))[0])
            {
                Id = reader.GetGuid(0),
                ParticipantType = type,
            };
        }

        new public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter { Value = Name, NpgsqlDbType = NpgsqlDbType.Text },
                new NpgsqlParameter { Value = (short)ParticipantType.Ekvipage, NpgsqlDbType = NpgsqlDbType.Smallint },
                new NpgsqlParameter { Value = Entity, NpgsqlDbType = NpgsqlDbType.Uuid },
            };

            var dbColumnNames = new List<string>
            {
                "Name",
                "ParticipantType",
                "Entity"
            };

            return new Tuple<List<string>, List<NpgsqlParameter>>(dbColumnNames, parameters);
        }
    }
}