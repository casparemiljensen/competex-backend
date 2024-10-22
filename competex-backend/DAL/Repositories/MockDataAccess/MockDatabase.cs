using competex_backend.Models;
using System.Xml.Linq;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockDatabase
    {
        public List<Club> Clubs { get; set; } = new List<Club>();
        public List<Member> Members { get; set; } = new List<Member>();
        public List<ClubMember> ClubMembers { get; set; } = new List<ClubMember>();

        private void SeedData()
        {
            // Mock data
            var member1 = new Member("Janni", "Karlsson");
            var member2 = new Member("Søren", "Pind");
            var member3 = new Member("Caspar", "Emil Jensen", new Guid("bec52019-b429-47bc-987e-47d13224d75e")) { Birthday = new DateTime(1990, 1, 1), Email = "Caspar@uni.com", Phone = "12345890", Permissions = "Admin" };
            var member4 = new Member("Thomas", "Ilum Andersen", new Guid("cd4d665d-cd71-4aaa-9799-9f9c973ce19e")) { Birthday = new DateTime(1985, 5, 23), Email = "Ilum@uni.com", Phone = "98763210", Permissions = "User" };
            var member5 = new Member("Thomas", "Dam Nykjær", new Guid("c7a53ea7-950a-4c8f-83c8-6262f2ec1571")) { Birthday = new DateTime(1995, 10, 10), Email = "Dam@uni.com", Phone = "55555555", Permissions = "Judge" };

            var club1 = new Club("Vejle Kaninhop","Kaninhop");
            var club2 = new Club("Lystrup Kaninhop","Kaninhop");
            var club3 = new Club("Kaninernes Klub Hjørring", "Kaninhop");
            var club4 = new Club("Aabybro kaninhop", "Kaninhop");
            var club5 = new Club("Aalborg kaninforening", "Kaninhop");
        

            var clubMember1 = new ClubMember { ClubId = club1.ClubId, MemberId = member1.MemberId, JoinDate = new DateTime(2011, 4, 20) };
            var clubMember2 = new ClubMember { ClubId = club1.ClubId, MemberId = member2.MemberId, JoinDate = new DateTime(2024, 1, 13) };
            var clubMember3 = new ClubMember { ClubId = club1.ClubId, MemberId = member3.MemberId, JoinDate = new DateTime(2015, 6, 17) };
            var clubMember4 = new ClubMember { ClubId = club2.ClubId, MemberId = member4.MemberId, JoinDate = new DateTime(2001, 8, 31) };
            var clubMember5 = new ClubMember { ClubId = club2.ClubId, MemberId = member5.MemberId, JoinDate = DateTime.UtcNow};

            Clubs.AddRange(new[] { club1, club2 });
            Members.AddRange(new[] { member1, member2 });
            ClubMembers.AddRange(new[] { clubMember1, clubMember2 });

            club1.ClubMembers.Add(clubMember1);
            club2.ClubMembers.Add(clubMember2);

            member1.ClubMembers.Add(clubMember1);
            member2.ClubMembers.Add(clubMember2);
        }
    }
}
