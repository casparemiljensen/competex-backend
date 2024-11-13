using competex_backend.Models;
using Member = competex_backend.Models.Member;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockDatabaseManager
    {
        public List<Member> Members { get; set; } = new();
        public List<Club> Clubs { get; set; } = new();
        public List<Round> Rounds { get; set; } = new();
        public List<SportType> SportTypes { get; set; } = new();
        public List<CompetitionType> CompetitionTypes { get; set; } = [];
        public List<Competition> Competitions { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        public List<ClubMembership> ClubMemberships { get; set; } = new();
        public List<Admin> Admins { get; set; } = new();
        public List<Entity> Entities { get; set; } = new();
        public List<Field> Fields { get; set; } = new();

        public MockDatabaseManager()
        {
            SeedData();
        }

        public List<T> GetEntities<T>() where T : class
        {
            if (typeof(T) == typeof(Member)) return (Members as List<T>)!;
            if (typeof(T) == typeof(Club)) return (Clubs as List<T>)!;
            if (typeof(T) == typeof(Round)) return (Rounds as List<T>)!;
            if (typeof(T) == typeof(SportType)) return (SportTypes as List<T>)!;
            if (typeof(T) == typeof(CompetitionType)) return (CompetitionTypes as List<T>)!;
            if (typeof(T) == typeof(Competition)) return (Competitions as List<T>)!;
            if (typeof(T) == typeof(Event)) return (Events as List<T>)!;
            if (typeof(T) == typeof(ClubMembership)) return (ClubMemberships as List<T>)!;
            if (typeof(T) == typeof(Admin)) return (Admins as List<T>)!;
            if (typeof(T) == typeof(Entity)) return (Entities as List<T>)!;
            throw new InvalidOperationException($"No collection found for type {typeof(T)}");
        }

        private void SeedData()
        {
            // Mock data

            #region members
            var member1 = new Member() { FirstName = "Janni", LastName = "Karlsson", Id = Guid.NewGuid() };
            var member2 = new Member() { FirstName = "Søren", LastName = "Pind", Id = Guid.NewGuid() };
            var member3 = new Member() { FirstName = "Caspar", LastName = "Emil Jensen", Id = new Guid("bec52019-b429-47bc-987e-47d13224d75e"), Birthday = new DateTime(1990, 1, 1), Email = "Caspar@uni.com", Phone = "12345890", Permissions = "Admin" };
            var member4 = new Member() { FirstName = "Thomas", LastName = "Ilum Andersen", Id = new Guid("cd4d665d-cd71-4aaa-9799-9f9c973ce19e"), Birthday = new DateTime(1985, 5, 23), Email = "Ilum@uni.com", Phone = "98763210", Permissions = "User" };
            var member5 = new Member() { FirstName = "Thomas", LastName = "Dam Nykjær", Id = new Guid("c7a53ea7-950a-4c8f-83c8-6262f2ec1571"), Birthday = new DateTime(1995, 10, 10), Email = "Dam@uni.com", Phone = "55555555", Permissions = "Judge" };
            Members.AddRange([member1, member2, member3, member4, member5]);
            #endregion

            #region clubs
            var club1 = new Club { Id = new Guid("23bfd5cb-9ee9-48c0-b1ae-8a9550223dcd"), Name = "Vejle Kaninhop", AssociatedSport = "Kaninhop" };
            var club2 = new Club { Id = Guid.NewGuid(), Name = "Lystrup Kaninhop", AssociatedSport = "Kaninhop" };
            var club3 = new Club { Id = Guid.NewGuid(), Name = "Kaninernes Klub Hjørring", AssociatedSport = "Kaninhop" };
            var club4 = new Club { Id = Guid.NewGuid(), Name = "Aabybro kaninhop", AssociatedSport = "Kaninhop" };
            var club5 = new Club { Id = Guid.NewGuid(), Name = "Aalborg kaninforening", AssociatedSport = "Kaninhop" };
            var club6 = new Club { Id = new Guid("aa57885f-cab9-48da-85d6-57a671c7d664"), Name = "Aalborg Håndbold", AssociatedSport = "Handball" };
            Clubs.AddRange([club1, club2, club3, club4, club5, club6]);
            #endregion

            #region sporttypes

            var sportType1 = new SportType
            {
                Id = new Guid("1035c83a-1899-49cb-bfd6-bcefc1aafffb"),
                Name = "Handball",
                EventAttributes = new List<string> { "Indoor", "Ball" },
                EntityType = EntityType.None
            };
            SportTypes.AddRange([sportType1]);

            #endregion

            #region admins
            var admin = new Admin
            {
                Id = member3.Id,
                FirstName = member3.FirstName,
                LastName = member3.LastName,
                Birthday = member3.Birthday,
                Email = member3.Email,
                Phone = member3.Phone,
                Permissions = member3.Permissions,
                SportTypes = new List<SportType> { sportType1 }
            };
            Admins.AddRange([admin]);

            #endregion

            #region clubmembers
            var clubMember1 = new ClubMembership { Id = Guid.NewGuid(), ClubId = club1.Id, MemberId = member1.Id, JoinDate = new DateTime(2011, 4, 20) };
            var clubMember2 = new ClubMembership { Id = Guid.NewGuid(), ClubId = club1.Id, MemberId = member2.Id, JoinDate = new DateTime(2024, 1, 13) };
            var clubMember3 = new ClubMembership { Id = Guid.NewGuid(), ClubId = club1.Id, MemberId = member3.Id, JoinDate = new DateTime(2015, 6, 17) };
            var clubMember4 = new ClubMembership { Id = Guid.NewGuid(), ClubId = club2.Id, MemberId = member4.Id, JoinDate = new DateTime(2001, 8, 31) };
            var clubMember5 = new ClubMembership { Id = Guid.NewGuid(), ClubId = club2.Id, MemberId = member5.Id, JoinDate = DateTime.UtcNow };

            ClubMemberships.AddRange([clubMember1, clubMember2, clubMember3, clubMember4, clubMember5]);
            #endregion

            #region entities
            var entity1 = new Entity
            {
                Id = new Guid("01b89faf-55f5-4707-bc2c-502484e7ed4a"),
                Owner = member3,
                Type = EntityType.Rabbit,
                Name = "Thumper",
                BirthDate = new DateTime(2021, 4, 15),
                Level = Level.Beginner,
            };

            var entity2 = new Entity
            {
                Id = new Guid("0b3c2028-ce7a-45c2-9fd4-da81f9f3c269"),
                Owner = member4,
                Type = EntityType.Rabbit,
                Name = "Cotton",
                BirthDate = new DateTime(2020, 8, 3),
                Level = Level.Intermediate,
            };

            var entity3 = new Entity
            {
                Id = new Guid("3cd7caa4-378d-4336-914e-c29d3ff40d85"),
                Owner = member3,
                Type = EntityType.Rabbit,
                Name = "Pepper",
                BirthDate = new DateTime(2022, 1, 20),
                Level = Level.Advanced,
            };

            var entity4 = new Entity
            {
                Owner = member4,
                Type = EntityType.Rabbit,
                Name = "Snowball",
                BirthDate = new DateTime(2019, 11, 25),
                Level = Level.Advanced,
            };

            var entity5 = new Entity
            {
                Owner = member5,
                Type = EntityType.Rabbit,
                Name = "Flopsy",
                BirthDate = new DateTime(2021, 6, 10),
                Level = Level.Beginner,
            };

            // Adding entities to the collection
            Entities.AddRange([entity1, entity2, entity3, entity4, entity5]);
            #endregion

            #region rounds
            var round1 = new Round { Name = "TestRoundOne", Id = new Guid("da9b7748-6278-4b97-b24e-716aec6aafac") };
            var round2 = new Round { Name = "TestRoundTwo", CompetitionId = new Guid("596462f8-2e32-4a21-921a-b5768c6b0d86") };
            var round3 = new Round { Id = Guid.NewGuid(), Name = "TestRoundThree" };
            var round4 = new Round { Id = Guid.NewGuid(), Name = "TestRoundFour" };
            var round5 = new Round { Id = Guid.NewGuid(), Name = "TestRoundFive" };
            Rounds.AddRange([round1, round2, round3, round4, round5]);
            #endregion

            #region fields
            var field1 = new Field()
            {
                Id = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                Name = "Bane 1",
                Location = "Hal 1",
                Capacity = 100,
                Surface = SurfaceType.NaturalGrass
            };

            var field2 = new Field
            {
                Id = new Guid("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"),
                Name = "Bane 2",
                Location = "Hal 2",
                Capacity = 80,
                Surface = SurfaceType.ArtificialTurf
            };

            var field3 = new Field
            {
                Id = new Guid("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"),
                Name = "Bane 3",
                Location = "Hal 3",
                Capacity = 120,
                Surface = SurfaceType.Clay
            };

            var field4 = new Field
            {
                Id = new Guid("4d5e6f7a-8b9c-0d1e-2f3a-4b5c6d7e8f9c"),
                Name = "Bane 4",
                Location = "Hal 4",
                Capacity = 60,
                Surface = SurfaceType.Dirt
            };

            var field5 = new Field
            {
                Id = new Guid("5e6f7a8b-9c0d-1e2f-3a4b-5c6d7e8f9d0b"),
                Name = "Bane 5",
                Location = "Hal 5",
                Capacity = 90,
                Surface = SurfaceType.Turf
            };

            var field6 = new Field
            {
                Id = new Guid(),
                Name = "Bane 1",
                Location = "Hal 1",
                Capacity = 2000,
                Surface = SurfaceType.PVC
            };

            Fields.AddRange([field1, field2, field3, field4, field5, field6]);
            #endregion

            #region competitions
            var comp1 = new Competition
            {
                Id = new Guid("da9b7748-6278-4b97-b24e-716aec6aafac"),
                CompetitionType = new List<CompetitionType> { new CompetitionType() },
                StartDate = new DateTime(2024, 5, 1),
                EndDate = new DateTime(2024, 5, 10),
                level = Level.Intermediate,
                Status = Status.Pending,
                MinParticipants = 5,
                MaxParticipants = 20
            };

            var comp2 = new Competition
            {
                Id = new Guid("42fff518-0815-4148-b826-33a4a1686dc0"),
                CompetitionType = new List<CompetitionType> { new CompetitionType { Id = new Guid("54cba910-da16-49ed-b4fc-865df9a2e47d"), Name = "Champions League Handball", ScoreType = ScoreType.Number, ScoreMethod = ScoreMethod.None } },
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 3),
                level = Level.Professional,
                Status = Status.Pending,
                MinParticipants = 10,
                MaxParticipants = 10
            };

            Competitions.AddRange([comp1, comp2]);
            #endregion

            #region events
            var mockEvent = new Event
            {
                Id = new Guid("27d4f28f-bd77-4bdc-865b-f13f7bbf71df"),
                Title = "Champions League Håndbold",
                Description = "Handball Finals",
                StartDate = new DateTime(2024, 6, 15, 9, 0, 0),
                EndDate = new DateTime(2024, 6, 15, 18, 0, 0),
                Location = new Location
                {
                    Name = "Sparekassen Danmark Arena",
                    Address = "Willy Brandts Vej 31",
                    Zip = "9000",
                    City = "Aalborg",
                    Country = "Denmark"
                },
                RegistrationStartDate = new DateTime(2024, 6, 10),
                RegistrationEndDate = new DateTime(2024, 6, 20),
                Status = Status.Pending,
                Organizer = Guid.Parse("aa57885f-cab9-48da-85d6-57a671c7d664"), // Mock ClubId as Organizer
                SportType = sportType1,
                Competitions = new List<Competition>() { comp2 }
            };
            Events.Add(mockEvent);
            #endregion

            #region CompetitionType
            var competitionTypeOne = new CompetitionType
            {
                Id = new Guid("5e6f7a8b-9c0d-1e2f-3a4b-5a6d7e8f9d0b"),
                Name = "CompetitionTypeOne",
                CompetitionAttributes = ["AttributeOne", "AttributeTwo"],
                ScoreMethod = ScoreMethod.C2,
                ScoreType = ScoreType.Time
            };
            var competitionTypeTwo = new CompetitionType
            {
                Id = new Guid("9c8b7a6d-5f4e-3a2d-1b0a-9d8c7b6f5a4e"),
                Name = "CompetitionTypeTwo",
                CompetitionAttributes = ["AttributeThree", "AttributeFour"],
                ScoreMethod = ScoreMethod.D1,
                ScoreType = ScoreType.Set
            };

            var competitionTypeThree = new CompetitionType
            {
                Id = new Guid("6f7e8d9c-4b3a-2c1d-0e9f-8a7b6c5d4e3f"),
                Name = "CompetitionTypeThree",
                CompetitionAttributes = ["AttributeFive", "AttributeSix"],
                ScoreMethod = ScoreMethod.C2,
                ScoreType = ScoreType.Number
            };

            var competitionTypeFour = new CompetitionType
            {
                Id = new Guid("3a2b1c0d-9e8f-7d6c-5b4a-3f2e1d0c9b8a"),
                Name = "CompetitionTypeFour",
                CompetitionAttributes = ["AttributeSeven", "AttributeEight"],
                ScoreMethod = ScoreMethod.D1,
                ScoreType = ScoreType.TimeAndPenalty
            };

            CompetitionTypes.AddRange([competitionTypeOne, competitionTypeTwo, competitionTypeThree, competitionTypeFour]);
            #endregion

            #region SportType
            var sportTypeOne = new SportType
            {
                Id = new Guid("3a2b1a0d-9e8f-7d6c-5b4a-3f2e1d0c9b8a"),
                Name = "SportTypeTestOne",
                EntityType = EntityType.Rabbit,
                EventAttributes = ["EventAttributeOne"],
            };
            var sportTypeTwo = new SportType
            {
                Id = new Guid("8b9d1a2c-3f4e-5d6b-7c8a-9e0f1d2a3b4c"),
                Name = "SportTypeTestTwo",
                EntityType = EntityType.Horse,
                EventAttributes = ["EventAttributeTwo", "EventAttributeThree"],
            };

            var sportTypeThree = new SportType
            {
                Id = new Guid("2e3f4d5b-6a7c-8d9e-0f1a-2b3c4d5e6f7a"),
                Name = "SportTypeTestThree",
                EntityType = EntityType.Rabbit,
                EventAttributes = ["EventAttributeFour"],
            };

            var sportTypeFour = new SportType
            {
                Id = new Guid("7c8d9e0f-1a2b-3c4d-5e6f-7a8b9c0d1e2f"),
                Name = "SportTypeTestFour",
                EntityType = EntityType.Horse,
                EventAttributes = ["EventAttributeFive", "EventAttributeSix"],
            };

            SportTypes.AddRange([sportTypeOne, sportTypeTwo, sportTypeThree, sportTypeFour]);
            #endregion
        }
    }
}
