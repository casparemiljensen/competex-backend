using Microsoft.EntityFrameworkCore;
using competex_backend.Models;
using Single = competex_backend.Models.Single;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        //public DbSet<Club> Clubs { get; set; }
        //public DbSet<Round> Rounds { get; set; }
        //public DbSet<SportType> SportTypes { get; set; }
        //public DbSet<CompetitionType> CompetitionTypes { get; set; }
        //public DbSet<Competition> Competitions { get; set; }
        //public DbSet<Event> Events { get; set; }
        //public DbSet<ClubMembership> ClubMemberships { get; set; }
        //public DbSet<Admin> Admins { get; set; }
        public DbSet<Entity> Entities { get; set; }
        //public DbSet<Field> Fields { get; set; }
        public DbSet<Location> Locations { get; set; }
        //public DbSet<Penalty> Penalties { get; set; }
        //public DbSet<Registration> Registrations { get; set; }
        //public DbSet<ScoringSystem> ScoringSystems { get; set; }
        public DbSet<Ekvipage> Participants { get; set; }
        public DbSet<Judge> Judges { get; set; }
        //public DbSet<Match> Matches { get; set; }
        //public DbSet<Score> Scores { get; set; }
        //public DbSet<ScoreResult> ScoreResults { get; set; }

        //public DbSet<DbParticipantMember> ParticipantMembers { get; set; }

        public DbSet<T> GetEntities<T>() where T : class
        {
            //Switch case?
            if (typeof(T) == typeof(Member)) return (Members as DbSet<T>)!;
            //if (typeof(T) == typeof(Club)) return (Clubs as DbSet<T>)!;
            //if (typeof(T) == typeof(Round)) return (Rounds as DbSet<T>)!;
            //if (typeof(T) == typeof(SportType)) return (SportTypes as DbSet<T>)!;
            //if (typeof(T) == typeof(CompetitionType)) return (CompetitionTypes as DbSet<T>)!;
            //if (typeof(T) == typeof(Competition)) return (Competitions as DbSet<T>)!;
            //if (typeof(T) == typeof(Event)) return (Events as DbSet<T>)!;
            //if (typeof(T) == typeof(ClubMembership)) return (ClubMemberships as DbSet<T>)!;
            //if (typeof(T) == typeof(Admin)) return (Admins as DbSet<T>)!;
            if (typeof(T) == typeof(Entity)) return (Entities as DbSet<T>)!;
            //if (typeof(T) == typeof(Field)) return (Fields as DbSet<T>)!;
            if (typeof(T) == typeof(Location)) return (Locations as DbSet<T>)!;
            //if (typeof(T) == typeof(Penalty)) return (Penalties as DbSet<T>)!;
            //if (typeof(T) == typeof(Registration)) return (Registrations as DbSet<T>)!;
            //if (typeof(T) == typeof(ScoringSystem)) return (ScoringSystems as DbSet<T>)!;
            if (typeof(T) == typeof(Ekvipage)) return (Participants as DbSet<T>)!;
            //if (typeof(T) == typeof(DbParticipantMember)) return (ParticipantMembers as DbSet<T>)!;
            if (typeof(T) == typeof(Judge)) return (Judges as DbSet<T>)!;
            //if (typeof(T) == typeof(Match)) return (Matches as DbSet<T>)!;
            //if (typeof(T) == typeof(Score)) return (Scores as DbSet<T>)!;
            //if (typeof(T) == typeof(ScoreResult)) return (ScoreResults as DbSet<T>)!;


            throw new InvalidOperationException($"No collection found for type {typeof(T)}");
        }

        //public async Task InitializeDatabaseAsync()
        //{
        //    await Database.EnsureCreatedAsync(); // Ensures that the schema matches the models
        //}

        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping entities to snake_case table names
            modelBuilder.Entity<Member>().ToTable("members");
            //modelBuilder.Entity<Club>().ToTable("clubs");
            //modelBuilder.Entity<Round>().ToTable("rounds");
            //modelBuilder.Entity<SportType>().ToTable("sport_types");
            //modelBuilder.Entity<CompetitionType>().ToTable("competition_types");
            //modelBuilder.Entity<Competition>().ToTable("competitions");
            //modelBuilder.Entity<Event>().ToTable("events");
            //modelBuilder.Entity<ClubMembership>().ToTable("club_memberships");
            //modelBuilder.Entity<Admin>().ToTable("admins");
            //modelBuilder.Entity<Entity>().ToTable("entities");
            //modelBuilder.Entity<Field>().ToTable("fields");
            modelBuilder.Entity<Location>().ToTable("locations");
            //modelBuilder.Entity<Penalty>().ToTable("penalties");
            //modelBuilder.Entity<Registration>().ToTable("registrations");
            //modelBuilder.Entity<ScoringSystem>().ToTable("scoring_systems");
            //modelBuilder.Entity<Participant>().ToTable("participants");
            //modelBuilder.Entity<Team>().ToTable("teams");
            //modelBuilder.Entity<Single>().ToTable("singles");
            //modelBuilder.Entity<Ekvipage>().ToTable("ekvipages");
            //modelBuilder.Entity<Match>().ToTable("matches");
            //modelBuilder.Entity<Score>().ToTable("scores");
            //modelBuilder.Entity<ScoreResult>().ToTable("score_results");


            modelBuilder.Entity<Judge>()
                .ToTable("judges")
                .HasKey(j => j.Id);

            modelBuilder.Entity<Judge>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(i => i.MemberId);

            modelBuilder.Entity<Entity>()
                .ToTable("entities")
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(s => s.OwnerId);

            modelBuilder.Entity<Ekvipage>()
                .ToTable("participants");

            modelBuilder.Entity<Ekvipage>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(e => e.MemberId);

            modelBuilder.Entity<Ekvipage>()
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(e => e.EntityId);


            modelBuilder.Entity<Score>()
                .UseTphMappingStrategy() // Default: Table-Per-Hierarchy (TPH)
                .HasDiscriminator<string>("ScoreType") // Add a discriminator column
                .HasValue<TimeScore>("TimeScore")
                .HasValue<SetScore>("SetScore")
                .HasValue<PointScore>("PointScore")
                .HasValue<TimeFaultScore>("TimeFaultScore");


            base.OnModelCreating(modelBuilder);
        }

    }
}
