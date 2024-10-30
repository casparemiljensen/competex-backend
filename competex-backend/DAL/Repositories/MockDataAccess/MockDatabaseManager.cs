using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Xml.Linq;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockDatabaseManager : IDatabaseManager
    {
        public List<Club> Clubs { get; set; } = new();
        public List<Member> Members { get; set; } = new();
        public List<ClubMember> ClubMembers { get; set; } = new();
        public List<Entity> Entities { get; set; } = new();
        public List<Field> Fields { get; set; } = new();

        public MockDatabaseManager()
        {
            SeedData();
        }

        private void SeedData()
        {
            // Mock data

            #region members
            var member1 = new Member("Janni", "Karlsson");
            var member2 = new Member("Søren", "Pind");
            var member3 = new Member("Caspar", "Emil Jensen", new Guid("bec52019-b429-47bc-987e-47d13224d75e")) { Birthday = new DateTime(1990, 1, 1), Email = "Caspar@uni.com", Phone = "12345890", Permissions = "Admin" };
            var member4 = new Member("Thomas", "Ilum Andersen", new Guid("cd4d665d-cd71-4aaa-9799-9f9c973ce19e")) { Birthday = new DateTime(1985, 5, 23), Email = "Ilum@uni.com", Phone = "98763210", Permissions = "User" };
            var member5 = new Member("Thomas", "Dam Nykjær", new Guid("c7a53ea7-950a-4c8f-83c8-6262f2ec1571")) { Birthday = new DateTime(1995, 10, 10), Email = "Dam@uni.com", Phone = "55555555", Permissions = "Judge" };
            Members.AddRange(new[] { member1, member2, member3, member4, member5 });
            #endregion

            #region clubs
            var club1 = new Club("Vejle Kaninhop","Kaninhop");
            var club2 = new Club("Lystrup Kaninhop","Kaninhop");
            var club3 = new Club("Kaninernes Klub Hjørring", "Kaninhop");
            var club4 = new Club("Aabybro kaninhop", "Kaninhop");
            var club5 = new Club("Aalborg kaninforening", "Kaninhop");
            Clubs.AddRange(new[] { club1, club2, club3, club4, club5 });
            #endregion

            #region clubmembers
            var clubMember1 = new ClubMember { ClubId = club1.ClubId, MemberId = member1.MemberId, JoinDate = new DateTime(2011, 4, 20) };
            var clubMember2 = new ClubMember { ClubId = club1.ClubId, MemberId = member2.MemberId, JoinDate = new DateTime(2024, 1, 13) };
            var clubMember3 = new ClubMember { ClubId = club1.ClubId, MemberId = member3.MemberId, JoinDate = new DateTime(2015, 6, 17) };
            var clubMember4 = new ClubMember { ClubId = club2.ClubId, MemberId = member4.MemberId, JoinDate = new DateTime(2001, 8, 31) };
            var clubMember5 = new ClubMember { ClubId = club2.ClubId, MemberId = member5.MemberId, JoinDate = DateTime.UtcNow};

            ClubMembers.AddRange(new[] { clubMember1, clubMember2, clubMember3, clubMember4, clubMember5});
            #endregion

            #region entities
            var entity1 = new Entity(member3, new Guid("01b89faf-55f5-4707-bc2c-502484e7ed4a"))
            {
                Type = EntityType.Rabbit,
                Name = "Thumper",
                BirthDate = new DateTime(2021, 4, 15),
                Level = EntityLevel.Beginner,
            };

            var entity2 = new Entity(member4, new Guid("0b3c2028-ce7a-45c2-9fd4-da81f9f3c269"))
            {
                Type = EntityType.Rabbit,
                Name = "Cotton",
                BirthDate = new DateTime(2020, 8, 3),
                Level = EntityLevel.Intermediate,
            };

            var entity3 = new Entity(member3, new Guid("3cd7caa4-378d-4336-914e-c29d3ff40d85"))
            {
                Type = EntityType.Rabbit,
                Name = "Pepper",
                BirthDate = new DateTime(2022, 1, 20),
                Level = EntityLevel.Advanced,
            };

            var entity4 = new Entity(member4)
            {
                Type = EntityType.Rabbit,
                Name = "Snowball",
                BirthDate = new DateTime(2019, 11, 25),
                Level = EntityLevel.Advanced,
            };

            var entity5 = new Entity(member5)
            {
                Type = EntityType.Rabbit,
                Name = "Flopsy",
                BirthDate = new DateTime(2021, 6, 10),
                Level = EntityLevel.Beginner,
            };

            Entities.AddRange(new[] { entity1, entity2, entity3, entity4, entity5 });
            #endregion



            #region fields
            var field1 = new Field("Bane 1", new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"))
            {
                Location = "Hal 1",
                Capacity = 100,
                Surface = SurfaceType.NaturalGrass
            };

            var field2 = new Field("Bane 2", new Guid("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"))
            {
                Location = "Hal 2",
                Capacity = 80,
                Surface = SurfaceType.ArtificialTurf
            };

            var field3 = new Field("Bane 3", new Guid("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"))
            {
                Location = "Hal 3",
                Capacity = 120,
                Surface = SurfaceType.Clay
            };

            var field4 = new Field("Bane 4", new Guid("4d5e6f7a-8b9c-0d1e-2f3a-4b5c6d7e8f9c"))
            {
                Location = "Hal 4",
                Capacity = 60,
                Surface = SurfaceType.Dirt
            };

            var field5 = new Field("Bane 5", new Guid("5e6f7a8b-9c0d-1e2f-3a4b-5c6d7e8f9d0b"))
            {
                Location = "Hal 5",
                Capacity = 90,
                Surface = SurfaceType.Turf
            };

            Fields.AddRange(new[] { field1, field2, field3, field4, field5 });
            #endregion
        }
    }
}
