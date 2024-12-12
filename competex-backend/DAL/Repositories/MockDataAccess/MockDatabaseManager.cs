using competex_backend.Models;
using System;
using Member = competex_backend.Models.Member;
using Single = competex_backend.Models.Single;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockDatabaseManager
    {
        public List<Member> Members { get; set; } = new();
        public List<Club> Clubs { get; set; } = new();
        public List<Round> Rounds { get; set; } = new();
        public List<SportType> SportTypes { get; set; } = new();
        public List<CompetitionType> CompetitionTypes { get; set; } = new();
        public List<Competition> Competitions { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        public List<ClubMembership> ClubMemberships { get; set; } = new();
        public List<Admin> Admins { get; set; } = new();
        public List<Entity> Entities { get; set; } = new();
        public List<Field> Fields { get; set; } = new();
        public List<Location> Locations { get; set; } = new();
        public List<Penalty> Penalties { get; set; } = new();
        public List<Registration> Registrations { get; set; } = new();
        public List<ScoringSystem> ScoringSystems { get; set; } = new();
        public List<Ekvipage> Participants { get; set; } = new();
        public List<Judge> Judges { get; set; } = new();
        public List<Match> Matches { get; set; } = new();
        public List<Score> Scores { get; set; } = new();
        public List<ScoreResult> ScoreResults { get; set; } = new();


        public MockDatabaseManager()
        {
            SeedData();
        }

        public List<T> GetEntities<T>() where T : class
        {
            //Switch case?
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
            if (typeof(T) == typeof(Field)) return (Fields as List<T>)!;
            if (typeof(T) == typeof(Location)) return (Locations as List<T>)!;
            if (typeof(T) == typeof(Penalty)) return (Penalties as List<T>)!;
            if (typeof(T) == typeof(Registration)) return (Registrations as List<T>)!;
            if (typeof(T) == typeof(ScoringSystem)) return (ScoringSystems as List<T>)!;
            if (typeof(T) == typeof(Participant)) return (Participants as List<T>)!;
            if (typeof(T) == typeof(Judge)) return (Judges as List<T>)!;
            if (typeof(T) == typeof(Match)) return (Matches as List<T>)!;
            if (typeof(T) == typeof(Score)) return (Scores as List<T>)!;
            if (typeof(T) == typeof(ScoreResult)) return (ScoreResults as List<T>)!;
            throw new InvalidOperationException($"No collection found for type {typeof(T)}");
        }

        private void SeedData()
        {
            // Mock data

            #region Members
            var member1 = new Member() { FirstName = "Janni", LastName = "Karlsson", Id = new Guid("81a9d1b7-2d4c-4520-944c-36e129447c26") };
            var member2 = new Member() { FirstName = "Søren", LastName = "Pind", Id = new Guid("98c5c539-6367-4d54-94c7-8a25d4b80986") };
            var member3 = new Member() { FirstName = "Caspar", LastName = "Emil Jensen", Id = new Guid("bec52019-b429-47bc-987e-47d13224d75e"), Birthday = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), Email = "Caspar@uni.com", Phone = "12345890", Permissions = 3 };
            var member4 = new Member() { FirstName = "Thomas", LastName = "Ilum Andersen", Id = new Guid("cd4d665d-cd71-4aaa-9799-9f9c973ce19e"), Birthday = new DateTime(1985, 5, 23, 0, 0, 0, DateTimeKind.Utc), Email = "Ilum@uni.com", Phone = "98763210", Permissions = 1 };
            var member5 = new Member() { FirstName = "Thomas", LastName = "Dam Nykjær", Id = new Guid("c7a53ea7-950a-4c8f-83c8-6262f2ec1571"), Birthday = new DateTime(1995, 10, 10, 0, 0, 0, DateTimeKind.Utc), Email = "Dam@uni.com", Phone = "55555555", Permissions = 2 };
            Members.AddRange([member1, member2, member3, member4, member5]);
            #endregion

            #region Clubs
            var club1 = new Club { Id = new Guid("23bfd5cb-9ee9-48c0-b1ae-8a9550223dcd"), Name = "Vejle Kaninhop", AssociatedSport = "Kaninhop" };
            var club2 = new Club { Id = new Guid("a6dfb1fc-983c-4ee8-bb95-9eaff204e50d"), Name = "Lystrup Kaninhop", AssociatedSport = "Kaninhop" };
            var club3 = new Club { Id = new Guid("b70683da-8b73-442e-95f4-03c52d9f6767"), Name = "Kaninernes Klub Hjørring", AssociatedSport = "Kaninhop" };
            var club4 = new Club { Id = new Guid("a12a8aca-c902-4b5c-8cd9-03d539a59a69"), Name = "Aabybro kaninhop", AssociatedSport = "Kaninhop" };
            var club5 = new Club { Id = new Guid("823a7304-eca0-4b42-b25b-2d9584107cf3"), Name = "Aalborg kaninforening", AssociatedSport = "Kaninhop" };
            var club6 = new Club { Id = new Guid("aa57885f-cab9-48da-85d6-57a671c7d664"), Name = "Aalborg Håndbold", AssociatedSport = "Handball" };
            Clubs.AddRange([club1, club2, club3, club4, club5, club6]);
            #endregion

            #region Sporttypes

            var sportTypeOne = new SportType
            {
                Id = new Guid("3a2b1a0d-9e8f-7d6c-5b4a-3f2e1d0c9b8a"),
                Name = "SportTypeTestOne",
                EntityType = EntityType.Rabbit
                //EventAttributes = ["EventAttributeOne"],
            };
            var sportTypeTwo = new SportType
            {
                Id = new Guid("8b9d1a2c-3f4e-5d6b-7c8a-9e0f1d2a3b4c"),
                Name = "SportTypeTestTwo",
                EntityType = EntityType.Horse
                //EventAttributes = ["EventAttributeTwo", "EventAttributeThree"],
            };

            var sportTypeThree = new SportType
            {
                Id = new Guid("2e3f4d5b-6a7c-8d9e-0f1a-2b3c4d5e6f7a"),
                Name = "SportTypeTestThree",
                EntityType = EntityType.Rabbit
                //EventAttributes = ["EventAttributeFour"],
            };

            var sportTypeFour = new SportType
            {
                Id = new Guid("7c8d9e0f-1a2b-3c4d-5e6f-7a8b9c0d1e2f"),
                Name = "SportTypeTestFour",
                EntityType = EntityType.Horse
                //EventAttributes = ["EventAttributeFive", "EventAttributeSix"],
            };

            var sportTypeFive = new SportType
            {
                Id = new Guid("1035c83a-1899-49cb-bfd6-bcefc1aafffb"),
                Name = "Handball",
                //EventAttributes = new List<string> { "Indoor", "Ball" },
                EntityType = EntityType.None
            };

            SportTypes.AddRange([sportTypeOne, sportTypeTwo, sportTypeThree, sportTypeFour, sportTypeFive]);
            #endregion

            #region CompetitionTypes
            var competitionTypeOne = new CompetitionType
            {
                Id = new Guid("5e6f7a8b-9c0d-1e2f-3a4b-5a6d7e8f9d0b"),
                Name = "CompetitionTypeOne",
                //CompetitionAttributes = ["AttributeOne", "AttributeTwo"],
                ScoreMethod = ScoreMethod.C2,
                ScoreType = ScoreType.Time
            };
            var competitionTypeTwo = new CompetitionType
            {
                Id = new Guid("9c8b7a6d-5f4e-3a2d-1b0a-9d8c7b6f5a4e"),
                Name = "CompetitionTypeTwo",
                //CompetitionAttributes = ["AttributeThree", "AttributeFour"],
                ScoreMethod = ScoreMethod.D1,
                ScoreType = ScoreType.Set
            };

            var competitionTypeThree = new CompetitionType
            {
                Id = new Guid("6f7e8d9c-4b3a-2c1d-0e9f-8a7b6c5d4e3f"),
                Name = "CompetitionTypeThree",
                //CompetitionAttributes = ["AttributeFive", "AttributeSix"],
                ScoreMethod = ScoreMethod.C2,
                ScoreType = ScoreType.Number
            };

            var competitionTypeFour = new CompetitionType
            {
                Id = new Guid("3a2b1c0d-9e8f-7d6c-5b4a-3f2e1d0c9b8a"),
                Name = "CompetitionTypeFour",
                //CompetitionAttributes = ["AttributeSeven", "AttributeEight"],
                ScoreMethod = ScoreMethod.D1,
                ScoreType = ScoreType.TimeAndPenalty
            };

            CompetitionTypes.AddRange([competitionTypeOne, competitionTypeTwo, competitionTypeThree, competitionTypeFour]);
            #endregion

            #region Locations

            var location1 = new Location
            {
                Id = new Guid("38e8c49d-27b8-4616-896a-f10015be0ca0"),
                Name = "Vejle Stadion",
                Address = "Vejlevej 1",
                Zip = "7100",
                City = "Vejle",
                Country = "Denmark"
            };

            var location2 = new Location
            {
                Id = new Guid("7dac7524-c2e1-447c-a960-0b864f9754b1"),
                Name = "Aalborg Stadion",
                Address = "Aalborgvej 1",
                Zip = "9000",
                City = "Aalborg",
                Country = "Denmark"
            };

            var location3 = new Location
            {
                Id = new Guid("d4cd5dd0-7142-4bb0-97d8-627f00a8c9d2"),
                Name = "Sparekassen Danmark Arena",
                Address = "Willy Brandts Vej 31",
                Zip = "9000",
                City = "Aalborg",
                Country = "Denmark"
            };

            Locations.AddRange([location1, location2, location3]);
            #endregion

            #region Events
            var mockEvent1 = new Event
            {
                Id = new Guid("fae628cb-93ea-4f6e-b10e-f014b61a26e6"),
                Title = "Champions League Håndbold",
                Description = "Handball Finals",
                StartDate = new DateTime(2024, 6, 15, 9, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 6, 15, 18, 0, 0, DateTimeKind.Utc),
                LocationId = Guid.Parse("38e8c49d-27b8-4616-896a-f10015be0ca0"),
                RegistrationStartDate = new DateTime(2024, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                RegistrationEndDate = new DateTime(2024, 6, 20, 0, 0, 0, DateTimeKind.Utc),
                Status = Status.Pending,
                OrganizerId = Guid.Parse("aa57885f-cab9-48da-85d6-57a671c7d664"), // Mock ClubId as Organizer
                SportTypeId = sportTypeFive.Id,
                CompetitionIds = new List<Guid>() { }
            };


            var mockEvent2 = new Event
            {
                Id = new Guid("6b30fbd3-37f6-48c4-9edf-4cf0236c8faf"),
                Title = "Champions League Tennis",
                Description = "Tennis Finals",
                StartDate = new DateTime(2024, 4, 16, 9, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 4, 20, 18, 0, 0, DateTimeKind.Utc),
                LocationId = Guid.Parse("7dac7524-c2e1-447c-a960-0b864f9754b1"),
                RegistrationStartDate = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                RegistrationEndDate = new DateTime(2024, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                Status = Status.Pending,
                OrganizerId = Guid.Parse("aa57885f-cab9-48da-85d6-57a671c7d664"), // Mock ClubId as Organizer
                SportTypeId = sportTypeFive.Id,
                CompetitionIds = new List<Guid>() { }
            };
            Events.AddRange([mockEvent1, mockEvent2]);
            #endregion

            #region Competitions
            var comp1 = new Competition
            {
                Id = new Guid("da9b7748-6278-4b97-b24e-716aec6aafac"),
                CompetitionTypeId = competitionTypeOne.Id,
                EventId = mockEvent1.Id,
                Name = "SomeComp",
                StartDate = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                Level = Level.Intermediate,
                Status = Status.Pending,
                MinParticipants = 5,
                MaxParticipants = 20,
                RegistrationPrice = 100
            };

            var comp2 = new Competition
            {
                Id = new Guid("42fff518-0815-4148-b826-33a4a1686dc0"),
                CompetitionTypeId = competitionTypeTwo.Id,
                EventId = mockEvent1.Id,
                Name = "SomeComp2",
                StartDate = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 7, 3, 0, 0, 0, DateTimeKind.Utc),
                Level = Level.Professional,
                Status = Status.Pending,
                MinParticipants = 10,
                MaxParticipants = 10,
                RegistrationPrice = 200
            };

            Competitions.AddRange([comp1, comp2]);
            #endregion

            #region Event-add-competitions

            mockEvent1.CompetitionIds.AddRange([comp1.Id, comp2.Id]);

            #endregion

            #region Rounds
            var round1 = new Round { Id = new Guid("da9b7748-6278-4b97-b24e-716aec6aafac"), Name = "TestRoundZero", CompetitionId = comp1.Id, SequenceNumber = 0 };
            var round2 = new Round { Id = new Guid("2b95ebd4-f93e-40c7-9f90-5b400f33584c"), Name = "TestRoundOne", CompetitionId = comp1.Id, SequenceNumber = 1 };
            var round3 = new Round { Id = new Guid("b77dac6f-8176-4779-ae18-8aefb2fa8298"), Name = "TestRoundTwo", CompetitionId = comp1.Id, SequenceNumber = 2 };
            var round4 = new Round { Id = new Guid("7ab6a7c2-e02f-42af-804b-22098cf91c36"), Name = "TestRoundThree", CompetitionId = comp2.Id, SequenceNumber = 3 };
            var round5 = new Round { Id = new Guid("3003501a-ab36-4a85-bb3e-8898e5da6ff5"), Name = "TestRoundFour", CompetitionId = comp2.Id, SequenceNumber = 4 };
            Rounds.AddRange([round1, round2, round3, round4, round5]);
            #endregion

            #region Clubmembers
            var clubMember1 = new ClubMembership { Id = new Guid("eb3f5d90-a280-4cee-ab4c-f7ea8085a931"), ClubId = club1.Id, MemberId = member1.Id, JoinDate = new DateTime(2011, 4, 20, 0, 0, 0, DateTimeKind.Utc) };
            var clubMember2 = new ClubMembership { Id = new Guid("9bef9306-8837-42ed-9b76-050bd94bd789"), ClubId = club1.Id, MemberId = member2.Id, JoinDate = new DateTime(2024, 1, 13, 0, 0, 0, DateTimeKind.Utc) };
            var clubMember3 = new ClubMembership { Id = new Guid("aac95ff7-a7c0-46d9-badc-e5e9e85da451"), ClubId = club1.Id, MemberId = member3.Id, JoinDate = new DateTime(2015, 6, 17, 0, 0, 0, DateTimeKind.Utc) };
            var clubMember4 = new ClubMembership { Id = new Guid("87140a0d-44a8-476a-90a8-a0cb4867f81a"), ClubId = club2.Id, MemberId = member4.Id, JoinDate = new DateTime(2001, 8, 31, 0, 0, 0, DateTimeKind.Utc) };
            var clubMember5 = new ClubMembership { Id = new Guid("3cfbea78-7b9c-4855-98f6-8c0dcb860140"), ClubId = club2.Id, MemberId = member5.Id, JoinDate = DateTime.UtcNow };

            ClubMemberships.AddRange([clubMember1, clubMember2, clubMember3, clubMember4, clubMember5]);
            #endregion

            #region Admins
            var admin = new Admin
            {
                Id = member3.Id,
                FirstName = member3.FirstName,
                LastName = member3.LastName,
                Birthday = member3.Birthday,
                Email = member3.Email,
                Phone = member3.Phone,
                Permissions = member3.Permissions,
                SportTypeIds = new List<Guid> { sportTypeFive.Id }
            };
            Admins.AddRange([admin]);

            #endregion

            #region Entities
            var entity1 = new Entity
            {
                Id = new Guid("01b89faf-55f5-4707-bc2c-502484e7ed4a"),
                OwnerId = member3.Id,
                Type = EntityType.Rabbit,
                Name = "Thumper",
                Birthdate = new DateTime(2021, 4, 15, 0, 0, 0, DateTimeKind.Utc),
                Level = Level.Beginner,
            };

            var entity2 = new Entity
            {
                Id = new Guid("0b3c2028-ce7a-45c2-9fd4-da81f9f3c269"),
                OwnerId = member4.Id,
                Type = EntityType.Rabbit,
                Name = "Cotton",
                Birthdate = new DateTime(2020, 8, 3, 0, 0, 0, DateTimeKind.Utc),
                Level = Level.Intermediate,
            };

            var entity3 = new Entity
            {
                Id = new Guid("3cd7caa4-378d-4336-914e-c29d3ff40d85"),
                OwnerId = member3.Id,
                Type = EntityType.Rabbit,
                Name = "Pepper",
                Birthdate = new DateTime(2022, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Level = Level.Advanced,
            };

            var entity4 = new Entity
            {
                Id = new Guid("5d5995e7-a0a6-4100-9ed6-1f79c7e69796"),
                OwnerId = member4.Id,
                Type = EntityType.Rabbit,
                Name = "Snowball",
                Birthdate = new DateTime(2019, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                Level = Level.Advanced,
            };

            var entity5 = new Entity
            {
                Id = new Guid("c7cd30af-5f23-44ba-912f-259a7a15a2ae"),
                OwnerId = member5.Id,
                Type = EntityType.Rabbit,
                Name = "Flopsy",
                Birthdate = new DateTime(2021, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                Level = Level.Beginner,
            };

            // Adding entities to the collection
            Entities.AddRange([entity1, entity2, entity3, entity4, entity5]);
            #endregion

            #region Fields
            var field1 = new Field()
            {
                Id = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                Name = "Bane 1",
                Location = "Hal 1",
                Capacity = 100,
                SurfaceType = SurfaceType.NaturalGrass
            };

            var field2 = new Field
            {
                Id = new Guid("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"),
                Name = "Bane 2",
                Location = "Hal 2",
                Capacity = 80,
                SurfaceType = SurfaceType.ArtificialTurf
            };

            var field3 = new Field
            {
                Id = new Guid("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"),
                Name = "Bane 3",
                Location = "Hal 3",
                Capacity = 120,
                SurfaceType = SurfaceType.Clay
            };

            var field4 = new Field
            {
                Id = new Guid("4d5e6f7a-8b9c-0d1e-2f3a-4b5c6d7e8f9c"),
                Name = "Bane 4",
                Location = "Hal 4",
                Capacity = 60,
                SurfaceType = SurfaceType.Dirt
            };

            var field5 = new Field
            {
                Id = new Guid("5e6f7a8b-9c0d-1e2f-3a4b-5c6d7e8f9d0b"),
                Name = "Bane 5",
                Location = "Hal 5",
                Capacity = 90,
                SurfaceType = SurfaceType.Turf
            };

            var field6 = new Field
            {
                Id = new Guid("ecb090df-095a-414c-a0f2-ac96ab94c283"),
                Name = "Bane 1",
                Location = "Hal 1",
                Capacity = 2000,
                SurfaceType = SurfaceType.PVC
            };

            Fields.AddRange([field1, field2, field3, field4, field5, field6]);
            #endregion

            #region Penalties

            var penalty1 = new Penalty
            {
                Id = new Guid("60b39302-657e-4b04-8a0b-93853bd6e7e3"),
                PenaltyType = PenaltyType.Time,
                PenaltyValue = 10
            };

            var penalty2 = new Penalty
            {
                Id = new Guid("d76c2677-900c-4768-947f-601153eb1769"),
                PenaltyType = PenaltyType.Distance,
                PenaltyValue = 5
            };

            var penalty3 = new Penalty
            {
                Id = new Guid("ef9d0397-59b5-4ac1-b5c5-f2a9273944ed"),
                PenaltyType = PenaltyType.Points,
                PenaltyValue = 2
            };

            Penalties.AddRange([penalty1, penalty2, penalty3]);
            #endregion

            #region ScoringSystem

            var scoringSystem1 = new ScoringSystem
            {
                Id = new Guid("5494ce3d-6745-4bc4-bae8-e3956eed5023"),
                Name = "ScoringSystemOne",
                Description = "Scoring System One",
                ScoreType = ScoreType.Number,
                ScoringRules = "Scoring Rules One",
                PenaltyIds = new List<Guid> { penalty3.Id },
                EvaluationMethod = (scoreType, penalties) => scoreType switch
                {
                    ScoreType.Number => 10,
                    ScoreType.Time => 20,
                    ScoreType.Set => 30,
                    _ => 0
                }
            };

            ScoringSystems.AddRange([scoringSystem1]);
            #endregion

            #region Participants

            // TODO: Reconsider the contructor of the Participant classes

            //var team1 = new Team("Team1", new List<Guid> { member1.Id, member2.Id });
            //team1.Id = Guid.NewGuid();

            //var team2 = new Team("Team2", new List<Guid> { member3.Id, member4.Id });
            //team2.Id = Guid.NewGuid();

            //var single1 = new Single("Single1", member3.Id);
            //single1.Id = Guid.NewGuid();

            //var single2 = new Single("Single2", member4.Id);
            //single2.Id = Guid.NewGuid();

            var ekvipage1 = new Ekvipage("ekvipage", member4.Id, entity1.Id);
            ekvipage1.Id = new Guid("5494ce3d-6745-4bc4-bae8-e3956eed5023");

            var ekvipage2 = new Ekvipage("ekvipage2", member5.Id, entity2.Id);
            ekvipage2.Id = new Guid("5c566aaf-81d0-447e-9867-9a6adcaaa8aa");

            var ekvipage3 = new Ekvipage("ekvipage3", member4.Id, entity1.Id);
            ekvipage3.Id = new Guid("4a20a238-489a-4a3a-835f-17a66d658b54");

            var ekvipage4 = new Ekvipage("ekvipage4", member5.Id, entity1.Id);
            ekvipage4.Id = new Guid("e395073e-f2ba-40dd-a295-7be3336f1bfa");

            //Participants.AddRange([team1, team2, single1, single2, ekvipage1, ekvipage2, ekvipage3, ekvipage4]);
            Participants.AddRange([ekvipage1, ekvipage2, ekvipage3, ekvipage4]);

            #endregion

            #region Registration
            var reg1 = new Registration
            {
                Id = new Guid("b5b89bca-3a9d-45c9-921d-01d9d04694c9"),
                CompetitionId = comp2.Id,
                ParticipantId = ekvipage1.Id
            };

            var reg2 = new Registration
            {
                Id = new Guid("80325be7-4486-4d13-84f6-8a4bd1a242ab"),
                CompetitionId = comp1.Id,
                ParticipantId = ekvipage1.Id,
                Status = RegistrationStatus.Accepted
            };

            var reg3 = new Registration
            {
                Id = new Guid("a171552e-0775-4391-9bcc-a6479c36133d"),
                CompetitionId = comp1.Id,
                ParticipantId = ekvipage2.Id,
                Status = RegistrationStatus.Accepted
            };
            
            Registrations.AddRange([reg1, reg2, reg3]);

            #endregion

            #region Judges

            var judge1 = new Judge
            {
                Id = new Guid("c1e4a90f-71e6-4aa3-a62c-73452290cd90"),
                JudgeType = JudgeType.Main,
                MemberId = member1.Id,
                Description = "Main Judge"
            };

            var judge2 = new Judge
            {
                Id = new Guid("16651790-0644-4025-b3c6-1c29477eb5d6"),
                JudgeType = JudgeType.Assistant,
                MemberId = member2.Id,
                Description = "Assistant Judge"
            };

            Judges.AddRange([judge1, judge2]);

            #endregion

            #region Matches
            var match1 = new Match
            {
                Id = new Guid("aa5144ab-c138-4bb9-9817-9b3a65521387"),
                RoundId = round1.Id,
                ParticipantIds = new List<Guid>
                {
                    Participants[0].Id, Participants[1].Id
                },
                Status = MatchStatus.Pending,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                FieldId = field1.Id,
                JudgeId = judge1.Id,
            };


            var match2 = new Match
            {
                Id = new Guid("4efd8e9d-d79c-4751-96eb-7d50eb5953ca"),
                RoundId = round1.Id,
                ParticipantIds = new List<Guid>
                {
                    Participants[2].Id, Participants[3].Id
                },
                Status = MatchStatus.Concluded,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                FieldId = field2.Id,
                JudgeId = judge2.Id,
            };

            var match3 = new Match
            {
                Id = new Guid("9c701cd9-b25e-437c-b210-d6b5a36ed4a6"),
                RoundId = round2.Id,
                ParticipantIds = new List<Guid>
                {
                    Participants[0].Id, Participants[1].Id
                },
                Status = MatchStatus.Concluded,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                FieldId = field2.Id,
                JudgeId = judge2.Id
            };

            var match4 = new Match
            {
                Id = new Guid("96216a14-9219-4a02-8fb9-ae420b92ffda"),
                RoundId = round2.Id,
                ParticipantIds = new List<Guid>
                {
                    Participants[2].Id, Participants[3].Id
                },
                Status = MatchStatus.Concluded,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                FieldId = field2.Id,
                JudgeId = judge2.Id
            };

            Matches.AddRange([match1, match2, match3, match4]);


            #endregion

            #region Scores

            // TODO: Reconsider the contructor of the Score classes


            //var score1a = new TimeScore(TimeSpan.FromMinutes(10), match1.Id, match1.ParticipantIds[0]);
            //score1a.Id = Guid.NewGuid();

            //var score1b = new TimeScore(TimeSpan.FromMinutes(9), match1.Id, match1.ParticipantIds[1]);
            //score1b.Id = Guid.NewGuid();

            //var score2a = new SetScore(2, match2.Id, match2.ParticipantIds[0]);
            //score2a.Id = Guid.NewGuid();

            //var score2b = new SetScore(3, match2.Id, match2.ParticipantIds[1]);
            //score2b.Id = Guid.NewGuid();

            //var score3a = new PointScore(10, match3.Id, match3.ParticipantIds[0]);
            //score3a.Id = Guid.NewGuid();

            //var score3b = new PointScore(9, match3.Id, match3.ParticipantIds[1]);
            //score3b.Id = Guid.NewGuid();

            var score4 = new TimeScore(TimeSpan.FromMinutes(10), match4.Id, ekvipage4.Id);
            score4.Id = new Guid("2bcb5f39-ae09-4114-adf5-5191b8e9a399");

            var score5 = new PointScore(10, match3.Id, ekvipage4.Id);
            score5.Id = new Guid("9ba00f8f-42b9-4a9e-bd32-8a9511442a8c");

            var score6 = new SetScore(2, match3.Id, ekvipage3.Id);
            score6.Id = new Guid("f6c92a3a-6a8a-430b-a881-c6f07e736ef8");

            var score7 = new TimeFaultScore(2, TimeSpan.FromMinutes(2), match1.Id, ekvipage1.Id);
            score7.Id = new Guid("ba98ef6e-3062-4a36-b088-e41c3d5b9566");

            var score8 = new TimeFaultScore(2, TimeSpan.FromMinutes(2), match2.Id, ekvipage2.Id);
            score8.Id = new Guid("1fcd910b-bc34-4102-9349-c455cdd3f0b3");

            //Scores.AddRange([score1a, score1b, score2a, score2b, score3a, score3b, score4]);
            Scores.AddRange([score4, score5, score6, score7, score8]);


            //Matches[0].ScoreIds.AddRange([score7.Id, score8.Id]);

            #endregion

            #region ScoreResults


            var scoreResult1 = new ScoreResult
            {
                Id = new Guid("6afa9c2c-932f-43f2-8497-1ee61926d11b"),
                CompetitionId = comp1.Id,
                ParticipantId = ekvipage1.Id,
                Faults = 2,
                Time = TimeSpan.FromMinutes(2)
            };

            ScoreResults.AddRange([scoreResult1]);

            #endregion
        }
    }
}
