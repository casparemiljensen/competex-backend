using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.DAL.Interfaces;
using competex_backend;
using competex_backend.BLL.Services;
using competex_backend.BLL.Interfaces;
using competex_backend.API.DTOs;
using competex_backend.Models;
using competex_backend.Common.ErrorHandling;
using competex_backend.DAL.Repositories.PostgressDataAccess;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .AllowAnyHeader()
                          .AllowAnyOrigin()
                          .AllowAnyMethod();
                      });
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.UseAllOfForInheritance();
    //c.UseAllOfToExtendReferenceSchemas(); // TESTING

    c.UseOneOfForPolymorphism();

    // Dynamically discover subtypes for any base type (e.g., ParticipantDTO, ScoreDTO)
    c.SelectSubTypesUsing(baseType =>
    {
        // Use the assembly of the provided base type to find its subtypes
        return baseType.Assembly.GetTypes()
            .Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract);
    });

    c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
});

builder.Services.AddSingleton<MockDatabaseManager>();

# region IGenericRepository
//Registers IGenericRepository<T> with specific implementations (MockMemberRepository, MockClubRepository, etc.) for each model type.
builder.Services.AddScoped<IGenericRepository<Member>, PostgresMemberRepository>(); //
builder.Services.AddScoped<IGenericRepository<Club>, MockClubRepository>();
builder.Services.AddScoped<IGenericRepository<Round>, MockRoundRepository>();
builder.Services.AddScoped<IGenericRepository<SportType>, MockSportTypeRepository>();
builder.Services.AddScoped<IGenericRepository<CompetitionType>, MockCompetitionTypeRepository>();
builder.Services.AddScoped<IGenericRepository<Competition>, MockCompetitionRepository>();
builder.Services.AddScoped<IGenericRepository<Event>, MockEventRepository>();
builder.Services.AddScoped<IGenericRepository<ClubMembership>, MockClubMembershipRepository>();
builder.Services.AddScoped<IGenericRepository<Admin>, MockAdminRepository>();
builder.Services.AddScoped<IGenericRepository<Entity>, PostgresEntityRepository>();
builder.Services.AddScoped<IGenericRepository<Field>, MockFieldRepository>();
builder.Services.AddScoped<IGenericRepository<Location>, PostgresLocationRepository>(); //
builder.Services.AddScoped<IGenericRepository<Penalty>, MockPenaltyRepository>();
builder.Services.AddScoped<IGenericRepository<Registration>, MockRegistrationRepository>();
builder.Services.AddScoped<IGenericRepository<ScoringSystem>, MockScoringSystemRepository>();
builder.Services.AddScoped<IGenericRepository<Ekvipage>, PostgresParticipantRepository>(); //
builder.Services.AddScoped<IGenericRepository<Judge>, PostgresJudgeRepository>(); //
builder.Services.AddScoped<IGenericRepository<Match>, MockMatchRepository>();
builder.Services.AddScoped<IGenericRepository<Score>, MockScoreRepository>();
builder.Services.AddScoped<IGenericRepository<ScoreResult>, MockScoreResultRepository>();

//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


#endregion

# region Services 
// Registers services specific to each model's business logic (such as IMemberService for Member, IClubService for Club, etc.).
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IRoundService, RoundService>();
builder.Services.AddScoped<ISportTypeService, SportTypeService>();
builder.Services.AddScoped<ICompetitionTypeService, CompetitionTypeService>();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IClubMembershipService, ClubMembershipService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IEntityService, EntityService>();
builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPenaltyService, PenaltyService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IScoringSystemService, ScoringSystemService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IJudgeService, JudgeService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IScoreResultService, ScoreResultService>();
# endregion

#region Service DTO Mappings
// Registers services with DTO mappings for each model type.
builder.Services.AddScoped<IMemberRepository, PostgresMemberRepository>(); //
builder.Services.AddScoped<IClubRepository, MockClubRepository>();
builder.Services.AddScoped<IRoundRepository, MockRoundRepository>();
builder.Services.AddScoped<ISportTypeRepository, MockSportTypeRepository>();
builder.Services.AddScoped<ICompetitionTypeRepository, MockCompetitionTypeRepository>();
builder.Services.AddScoped<ICompetitionRepository, MockCompetitionRepository>();
builder.Services.AddScoped<IEventRepository, MockEventRepository>();
builder.Services.AddScoped<IClubMembershipRepository, MockClubMembershipRepository>();
builder.Services.AddScoped<IAdminRepository, MockAdminRepository>();
builder.Services.AddScoped<IEntityRepository, PostgresEntityRepository>();
builder.Services.AddScoped<IFieldRepository, MockFieldRepository>();
builder.Services.AddScoped<ILocationRepository, PostgresLocationRepository>(); //
builder.Services.AddScoped<IPenaltyRepository, MockPenaltyRepository>();
builder.Services.AddScoped<IRegistrationRepository, MockRegistrationRepository>();
builder.Services.AddScoped<IScoringSystemRepository, MockScoringSystemRepository>();
builder.Services.AddScoped<IParticipantRepository, PostgresParticipantRepository>(); //
builder.Services.AddScoped<IJudgeRepository, PostgresJudgeRepository>(); //
builder.Services.AddScoped<IMatchRepository, MockMatchRepository>();
builder.Services.AddScoped<IScoreRepository, MockScoreRepository>();
builder.Services.AddScoped<IScoreResultRepository, MockScoreResultRepository>();


#endregion

# region IGenericService
// Register services with DTO mappings for IGenericService<TDto>
// Registers a GenericService to handle operations on DTOs (Data Transfer Objects) for each model type.
builder.Services.AddScoped<IGenericService<MemberDTO>, GenericService<Member, MemberDTO>>();
builder.Services.AddScoped<IGenericService<ClubDTO>, GenericService<Club, ClubDTO>>();
builder.Services.AddScoped<IGenericService<RoundDTO>, GenericService<Round, RoundDTO>>();
builder.Services.AddScoped<IGenericService<SportTypeDTO>, GenericService<SportType, SportTypeDTO>>();
builder.Services.AddScoped<IGenericService<CompetitionTypeDTO>, GenericService<CompetitionType, CompetitionTypeDTO>>();
builder.Services.AddScoped<IGenericService<CompetitionDTO>, GenericService<Competition, CompetitionDTO>>();
builder.Services.AddScoped<IGenericService<EventDTO>, GenericService<Event, EventDTO>>();
builder.Services.AddScoped<IGenericService<ClubMembershipDTO>, GenericService<ClubMembership, ClubMembershipDTO>>();
builder.Services.AddScoped<IGenericService<AdminDTO>, GenericService<Admin, AdminDTO>>();
builder.Services.AddScoped<IGenericService<EntityDTO>, GenericService<Entity, EntityDTO>>();
builder.Services.AddScoped<IGenericService<FieldDTO>, GenericService<Field, FieldDTO>>();
builder.Services.AddScoped<IGenericService<LocationDTO>, GenericService<Location, LocationDTO>>();
builder.Services.AddScoped<IGenericService<PenaltyDTO>, GenericService<Penalty, PenaltyDTO>>();
builder.Services.AddScoped<IGenericService<ScoringSystemDTO>, GenericService<ScoringSystem, ScoringSystemDTO>>();
builder.Services.AddScoped<IGenericService<RegistrationDTO>, GenericService<Registration, RegistrationDTO>>();
builder.Services.AddScoped<IGenericService<ScoringSystemDTO>, GenericService<ScoringSystem, ScoringSystemDTO>>();
builder.Services.AddScoped<IGenericService<EkvipageDTO>, GenericService<Ekvipage, EkvipageDTO>>();
builder.Services.AddScoped<IGenericService<JudgeDTO>, GenericService<Judge, JudgeDTO>>();
builder.Services.AddScoped<IGenericService<MatchDTO>, GenericService<Match, MatchDTO>>();
builder.Services.AddScoped<IGenericService<ScoreDTO>, GenericService<Score, ScoreDTO>>();
builder.Services.AddScoped<IGenericService<ScoreResultDTO>, GenericService<ScoreResult, ScoreResultDTO>>();
# endregion

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configure the database context for PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Apply migrations and seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // Apply pending migrations
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        // Log migration errors
        Console.WriteLine($"An error occurred during migration: {ex.Message}");
    }
}


// Configure the HTTP request pipeline.
// Right now we want to show Swagger UI in production. Remove this clause when that changes
if (app.Environment.IsDevelopment() || true)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // Collapse swagger on startup
        });
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.MapControllers();

app.Run();
