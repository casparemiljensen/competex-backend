using Microsoft.EntityFrameworkCore;
using competex_backend.Models;
using Single = competex_backend.Models.Single;
using competex_backend.Common.Helpers;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<CompetitionType> CompetitionTypes { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ClubMembership> ClubMemberships { get; set; }
        //public DbSet<Admin> Admins { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Location> Locations { get; set; }
        //public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        //public DbSet<ScoringSystem> ScoringSystems { get; set; }
        public DbSet<Ekvipage> Participants { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<ScoreResult> ScoreResults { get; set; }

        public DbSet<T> GetEntities<T>() where T : class
        {
            //Switch case?
            if (typeof(T) == typeof(Member)) return (Members as DbSet<T>)!;
            if (typeof(T) == typeof(Club)) return (Clubs as DbSet<T>)!;
            if (typeof(T) == typeof(Round)) return (Rounds as DbSet<T>)!;
            if (typeof(T) == typeof(SportType)) return (SportTypes as DbSet<T>)!;
            if (typeof(T) == typeof(CompetitionType)) return (CompetitionTypes as DbSet<T>)!;
            if (typeof(T) == typeof(Competition)) return (Competitions as DbSet<T>)!;
            if (typeof(T) == typeof(Event)) return (Events as DbSet<T>)!;
            if (typeof(T) == typeof(ClubMembership)) return (ClubMemberships as DbSet<T>)!;
            //if (typeof(T) == typeof(Admin)) return (Admins as DbSet<T>)!;
            if (typeof(T) == typeof(Entity)) return (Entities as DbSet<T>)!;
            if (typeof(T) == typeof(Field)) return (Fields as DbSet<T>)!;
            if (typeof(T) == typeof(Location)) return (Locations as DbSet<T>)!;
            //if (typeof(T) == typeof(Penalty)) return (Penalties as DbSet<T>)!;
            if (typeof(T) == typeof(Registration)) return (Registrations as DbSet<T>)!;
            //if (typeof(T) == typeof(ScoringSystem)) return (ScoringSystems as DbSet<T>)!;
            if (typeof(T) == typeof(Ekvipage)) return (Participants as DbSet<T>)!;
            if (typeof(T) == typeof(Judge)) return (Judges as DbSet<T>)!;
            if (typeof(T) == typeof(Match)) return (Matches as DbSet<T>)!;
            if (typeof(T) == typeof(Score)) return (Scores as DbSet<T>)!;
            if (typeof(T) == typeof(ScoreResult)) return (ScoreResults as DbSet<T>)!;


            throw new InvalidOperationException($"No collection found for type {typeof(T)}");
        }

        //public async Task InitializeDatabaseAsync()
        //{
        //    await Database.EnsureCreatedAsync(); // Ensures that the schema matches the models
        //}

        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Admin>().ToTable("admins");
            //modelBuilder.Entity<Penalty>().ToTable("penalties");
            //modelBuilder.Entity<ScoringSystem>().ToTable("scoring_systems");


            // Mapping entities to snake_case table names
            modelBuilder.Entity<Member>().ToTable("members");
            modelBuilder.Entity<Club>().ToTable("clubs");


            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable("rounds")
                .HasOne<Competition>()
                .WithMany()
                .HasForeignKey(e => e.CompetitionId);
            });

            modelBuilder.Entity<CompetitionType>().ToTable("competition_types");


            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("competitions")
                .HasOne<CompetitionType>()
                .WithMany()
                .HasForeignKey(e => e.CompetitionTypeId);

                entity
                .HasOne<Event>()
                .WithMany()
                .HasForeignKey(e => e.EventId);

            });



            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events")
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(e => e.LocationId);

                entity
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(e => e.OrganizerId);

                entity
                .HasOne<SportType>()
                .WithMany()
                .HasForeignKey(e => e.SportTypeId);

                // Configure the many-to-many relationship with Competition using a join table
                entity.HasMany<Competition>()
                      .WithMany()
                      .UsingEntity<Dictionary<string, object>>(
                          "event_competitions", // Name of the join table
                          j => j.HasOne<Competition>() // Configure Competition side
                               .WithMany()
                               .HasForeignKey("CompetitionId") // Foreign key for Competition
                               .OnDelete(DeleteBehavior.Cascade), // Cascade delete when Competition is deleted
                          j => j.HasOne<Event>() // Configure Event side
                               .WithMany()
                               .HasForeignKey("EventId") // Foreign key for Event
                               .OnDelete(DeleteBehavior.Cascade) // Cascade delete when Event is deleted
                      );
            });

            modelBuilder.Entity<SportType>().ToTable("sport_types");

            modelBuilder.Entity<Location>().ToTable("locations");

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("registrations")
                  .HasOne<Ekvipage>()
                  .WithMany()
                  .HasForeignKey(r => r.ParticipantId)
                  .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Competition>()
                  .WithMany()
                  .HasForeignKey(r => r.CompetitionId)
                  .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<ScoreResult>(entity =>
            {

                entity.ToTable("score_results")
                .HasOne<Competition>()
                .WithMany()
                .HasForeignKey(e => e.CompetitionId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Ekvipage>()
                .WithMany()
                .HasForeignKey(e => e.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);
            }
            );


            modelBuilder.Entity<Judge>()
                .ToTable(DatabaseHelper.GetTableName<Judge>())
                .HasKey(j => j.Id);

            modelBuilder.Entity<Judge>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(i => i.MemberId);

            modelBuilder.Entity<Entity>()
                .ToTable(DatabaseHelper.GetTableName<Entity>())
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(s => s.OwnerId);

            modelBuilder.Entity<Ekvipage>()
                .ToTable(DatabaseHelper.GetTableName<Participant>());

            modelBuilder.Entity<Ekvipage>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(e => e.MemberId);

            modelBuilder.Entity<Ekvipage>()
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(e => e.EntityId);

            modelBuilder.Entity<Score>(entity =>
            {
                entity.ToTable("scores")
                      .UseTphMappingStrategy()
                      .HasDiscriminator<short>("ScoreType")
                      .HasValue<TimeScore>(1)
                      .HasValue<SetScore>(2)
                      .HasValue<PointScore>(3)
                      .HasValue<TimeFaultScore>(4);


                // Ignore the abstract property
                entity.Ignore(s => s.ScoreValue);
                entity.Ignore(s => s.PenaltyIds);
            });

            // Derived class-specific properties
            modelBuilder.Entity<TimeScore>()
                .Property(i => i.Time)
                .HasColumnName("Time");

            modelBuilder.Entity<TimeScore>(entity =>
            {
                entity.HasOne<Match>()
                .WithMany()
                .HasForeignKey(e => e.MatchId);

                entity.HasOne<Ekvipage>()
                .WithMany()
                .HasForeignKey(e => e.ParticipantId);
            }
            );

            modelBuilder.Entity<SetScore>()
                .Property(i => i.SetsWon)
                .HasColumnName("Points");
            modelBuilder.Entity<PointScore>()
                .Property(i => i.Points)
                .HasColumnName("Points");

            modelBuilder.Entity<TimeFaultScore>(entity =>
            {
                entity.Property(i => i.Time).HasColumnName("Time");
                entity.Property(i => i.Faults).HasColumnName("Faults");
            });


            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("matches")
                   .HasOne<Round>()
                   .WithMany()
                   .HasForeignKey(e => e.RoundId);

                entity.HasMany<Ekvipage>()
                   .WithMany()
                   .UsingEntity<Dictionary<string, object>>(
                       "match_participants", // Name of the join table
                       j => j.HasOne<Ekvipage>() // Configure Participant side
                            .WithMany()
                            .HasForeignKey("ParticipantId"), // Foreign key for ParticipantId
                       j => j.HasOne<Match>() // Configure Match side
                            .WithMany()
                            .HasForeignKey("MatchId") // Foreign key for MatchId
                        );

                entity.HasOne<Field>()
                    .WithMany()
                    .HasForeignKey(e => e.FieldId);

                entity.HasOne<Judge>()
                    .WithMany()
                    .HasForeignKey(e => e.JudgeId);

                entity.Ignore(s => s.ParticipantIds);
            });

            modelBuilder.Entity<ClubMembership>(entity =>
            {
                entity.ToTable("club_memberships")
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(e => e.ClubId);

                entity.HasOne<Member>()
                .WithMany()
                .HasForeignKey(e => e.MemberId);
            });

            modelBuilder.Entity<Field>(entity => entity.ToTable("fields"));

            base.OnModelCreating(modelBuilder);
        }

    }
}
