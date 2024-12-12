using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.DAL.Repositories.PostgresDataAccess;

namespace competex_backend.DAL.Repositories
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(ApplicationDbContext context, MockDatabaseManager mockDatabaseManager)
        {
            // Ensure database exists
            context.Database.EnsureCreated();

            // Seed Members
            if (!context.Members.Any())
            {
                context.Members.AddRange(mockDatabaseManager.Members);
                context.SaveChanges();
            }

            if (!context.Clubs.Any())
            {
                context.Clubs.AddRange(mockDatabaseManager.Clubs);
                context.SaveChanges();
            }

            if (!context.SportTypes.Any())
            {
                context.SportTypes.AddRange(mockDatabaseManager.SportTypes);
                context.SaveChanges();
            }

            if (!context.CompetitionTypes.Any())
            {
                context.CompetitionTypes.AddRange(mockDatabaseManager.CompetitionTypes);
                context.SaveChanges();
            }

            if (!context.Locations.Any())
            {
                context.Locations.AddRange(mockDatabaseManager.Locations);
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(mockDatabaseManager.Events);
                context.SaveChanges();
            }

            if (!context.Competitions.Any())
            {
                context.Competitions.AddRange(mockDatabaseManager.Competitions);
                context.SaveChanges();
            }

            if (!context.Rounds.Any())
            {
                context.Rounds.AddRange(mockDatabaseManager.Rounds);
                context.SaveChanges();
            }

            if (!context.ClubMemberships.Any())
            {
                context.ClubMemberships.AddRange(mockDatabaseManager.ClubMemberships);
                context.SaveChanges();
            }

            //if (!context.Admins.Any())
            //{
            //    context.Admins.AddRange(mockDatabaseManager.Admins);
            //    context.SaveChanges();
            //}

            if (!context.Entities.Any())
            {
                context.Entities.AddRange(mockDatabaseManager.Entities);
                context.SaveChanges();
            }

            if (!context.Fields.Any())
            {
                context.Fields.AddRange(mockDatabaseManager.Fields);
                context.SaveChanges();
            }

            //if (!context.Penalties.Any())
            //{
            //    context.Penalties.AddRange(mockDatabaseManager.Penalties);
            //    context.SaveChanges();
            //}


            //if (!context.ScoringSystems.Any())
            //{
            //    context.ScoringSystems.AddRange(mockDatabaseManager.ScoringSystems);
            //    context.SaveChanges();
            //}

            if (!context.Participants.Any())
            {
                context.Participants.AddRange(mockDatabaseManager.Participants);
                context.SaveChanges();
            }

            if (!context.Registrations.Any())
            {
                context.Registrations.AddRange(mockDatabaseManager.Registrations);
                context.SaveChanges();
            }

            if (!context.Judges.Any())
            {
                context.Judges.AddRange(mockDatabaseManager.Judges);
                context.SaveChanges();
            }

            if (!context.Matches.Any())
            {
                context.Matches.AddRange(mockDatabaseManager.Matches);
                context.SaveChanges();
            }

            if (!context.Scores.Any())
            {
                context.Scores.AddRange(mockDatabaseManager.Scores);
                context.SaveChanges();
            }

            if (!context.ScoreResults.Any())
            {
                context.ScoreResults.AddRange(mockDatabaseManager.ScoreResults);
                context.SaveChanges();
            }

        }
    }
}

