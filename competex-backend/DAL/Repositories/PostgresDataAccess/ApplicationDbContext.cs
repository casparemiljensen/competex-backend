using Microsoft.EntityFrameworkCore;
using competex_backend.Models;
using Single = competex_backend.Models.Single;
using competex_backend.Common.Helpers;

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

        public async Task InitializeDatabaseAsync()
        {
            await Database.EnsureCreatedAsync(); // Ensures that the schema matches the models
        }

        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Admin>().ToTable("admins");
            //modelBuilder.Entity<Penalty>().ToTable("penalties");
            //modelBuilder.Entity<ScoringSystem>().ToTable("scoring_systems");

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable(DatabaseHelper.GetTableName<Member>());
                    //.Property(r => r.Birthday)
                    //.HasColumnType("timestamp");
            });

            modelBuilder.Entity<Club>().ToTable(DatabaseHelper.GetTableName<Club>());


            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable(DatabaseHelper.GetTableName<Round>())
                .HasOne<Competition>()
                .WithMany()
                .HasForeignKey(e => e.CompetitionId)
                .OnDelete(DeleteBehavior.Restrict);

                //entity.Property(r => r.StartTime)
                //.HasColumnType("timestamp");

                //entity.Property(r => r.EndTime)
                // .HasColumnType("timestamp");
            });

            modelBuilder.Entity<CompetitionType>().ToTable(DatabaseHelper.GetTableName<CompetitionType>());

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable(DatabaseHelper.GetTableName<Competition>())
                .HasOne<CompetitionType>()
                .WithMany()
                .HasForeignKey(e => e.CompetitionTypeId)
                .OnDelete(DeleteBehavior.SetNull);

                entity
                .HasOne<Event>()
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade);

                //entity.Property(r => r.StartDate)
                // .HasColumnType("timestamp");

                //entity.Property(r => r.EndDate)
                // .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                //entity.Property(r => r.StartDate)
                // .HasColumnType("timestamp");

                //entity.Property(r => r.EndDate)
                // .HasColumnType("timestamp");

                //entity.Property(r => r.RegistrationStartDate)
                // .HasColumnType("timestamp");

                //entity.Property(r => r.RegistrationEndDate)
                // .HasColumnType("timestamp");

                entity.ToTable(DatabaseHelper.GetTableName<Event>())
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

                entity
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .HasOne<SportType>()
                .WithMany()
                .HasForeignKey(e => e.SportTypeId)
                .OnDelete(DeleteBehavior.SetNull);

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

            modelBuilder.Entity<SportType>().ToTable(DatabaseHelper.GetTableName<SportType>());

            modelBuilder.Entity<Location>().ToTable(DatabaseHelper.GetTableName<Location>());

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable(DatabaseHelper.GetTableName<Registration>())
                  .HasOne<Ekvipage>()
                  .WithMany()
                  .HasForeignKey(r => r.ParticipantId)
                  .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Competition>()
                  .WithMany()
                  .HasForeignKey(r => r.CompetitionId)
                  .OnDelete(DeleteBehavior.Cascade);

                //entity.Property(r => r.RegistrationDate)
                //    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ScoreResult>(entity =>
            {

                entity.ToTable(DatabaseHelper.GetTableName<ScoreResult>())
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

            modelBuilder.Entity<Judge>(entity =>
            {
                entity.ToTable(DatabaseHelper.GetTableName<Judge>())
                .HasKey(j => j.Id);

                entity
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(i => i.MemberId);
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.ToTable(DatabaseHelper.GetTableName<Entity>())
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(s => s.OwnerId);

                //entity.Property(r => r.Birthdate)
                //    .HasColumnType("timestamp");

            });


            modelBuilder.Entity<Ekvipage>(entity =>
            {
                entity.ToTable("participants")
                    .HasOne<Member>()
                    .WithMany()
                    .HasForeignKey(e => e.MemberId);

                entity.HasOne<Entity>()
                    .WithMany()
                    .HasForeignKey(e => e.EntityId);

            });


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
                entity.ToTable(DatabaseHelper.GetTableName<Match>())
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


                //entity.Property(r => r.StartTime)
                //.HasColumnType("timestamp");

                //entity.Property(r => r.EndTime)
                //.HasColumnType("timestamp");

                entity.Ignore(s => s.ParticipantIds);
            });

            modelBuilder.Entity<ClubMembership>(entity =>
            {
                entity.ToTable(DatabaseHelper.GetTableName<ClubMembership>())
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(e => e.ClubId);

                entity.HasOne<Member>()
                .WithMany()
                .HasForeignKey(e => e.MemberId);

                //entity.Property(r => r.JoinDate)
                //.HasColumnType("timestamp");
            });

            modelBuilder.Entity<Field>(entity => entity.ToTable(DatabaseHelper.GetTableName<Field>()));

            base.OnModelCreating(modelBuilder);

            // Generate UUIDs for primary keys in db
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var primaryKey = entityType.FindPrimaryKey();
                if (primaryKey != null)
                {
                    foreach (var property in primaryKey.Properties)
                    {
                        if (property.ClrType == typeof(Guid))
                        {
                            property.SetDefaultValueSql("gen_random_uuid()");
                        }
                    }
                }
            }
        }
    }
}
