using competex_backend.Helpers;
using Npgsql;

namespace competex_backend.Models

{
    public class Member : Identifiable, IMappable<Member>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Permissions Permissions { get; set; } = 0; //Set to correct type when we figure out how to handle permissions
        // public ICollection<ClubMember> ClubMembers { get; set; } = new List<ClubMember>();

        // No club implementation yet. 

        public static Member Map(NpgsqlDataReader reader)
        {
            return new Member()
            {
                Id = reader.GetGuid(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Birthday = reader.GetDateTime(3),
                Email = reader.GetString(4),
                Phone = reader.GetString(5),
                Permissions = EnumMapper.MapEnumValueTo<Permissions>(reader.GetInt16(6)).GetValueOrDefault()
            };
        }
    }
}
