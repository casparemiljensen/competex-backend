using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.DAL.Interfaces;
using competex_backend;
using competex_backend.BLL.Services;
using competex_backend.BLL.Interfaces;
using competex_backend.API.DTOs;
using competex_backend.Models;
using competex_backend.Common.ErrorHandling;
using competex_backend.DAL.Repositories.PostgresDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using competex_backend.DAL.Repositories;


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
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.UseAllOfForInheritance();

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
builder.Services.AddScoped<IGenericRepository<Member>, PostgresMemberRepository>();
builder.Services.AddScoped<IGenericRepository<Club>, PostgresClubRepository>();
builder.Services.AddScoped<IGenericRepository<Round>, PostgresRoundRepository>();
builder.Services.AddScoped<IGenericRepository<SportType>, PostgresSportTypeRepository>();
builder.Services.AddScoped<IGenericRepository<CompetitionType>, PostgresCompetitionTypeRepository>();
builder.Services.AddScoped<IGenericRepository<Competition>, PostgresCompetitionRepository>();
builder.Services.AddScoped<IGenericRepository<Event>, PostgresEventRepository>();
builder.Services.AddScoped<IGenericRepository<ClubMembership>, PostgresClubMembershipRepository>();
builder.Services.AddScoped<IGenericRepository<Admin>, MockAdminRepository>(); // MOCK
builder.Services.AddScoped<IGenericRepository<Entity>, PostgresEntityRepository>();
builder.Services.AddScoped<IGenericRepository<Field>, PostgresFieldRepository>();
builder.Services.AddScoped<IGenericRepository<Location>, PostgresLocationRepository>();
builder.Services.AddScoped<IGenericRepository<Penalty>, MockPenaltyRepository>(); // MOCK
builder.Services.AddScoped<IGenericRepository<Registration>, PostgresRegistrationRepository>();
builder.Services.AddScoped<IGenericRepository<ScoringSystem>, MockScoringSystemRepository>(); // MOCK
builder.Services.AddScoped<IGenericRepository<Ekvipage>, PostgresParticipantRepository>();
builder.Services.AddScoped<IGenericRepository<Judge>, PostgresJudgeRepository>();
builder.Services.AddScoped<IGenericRepository<Match>, PostgresMatchRepository>();
builder.Services.AddScoped<IGenericRepository<Score>, PostgresScoreRepository>();
builder.Services.AddScoped<IGenericRepository<ScoreResult>, PostgresScoreResultRepository>();


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
builder.Services.AddScoped<IMemberRepository, PostgresMemberRepository>();
builder.Services.AddScoped<IClubRepository, PostgresClubRepository>();
builder.Services.AddScoped<IRoundRepository, PostgresRoundRepository>();
builder.Services.AddScoped<ISportTypeRepository, PostgresSportTypeRepository>();
builder.Services.AddScoped<ICompetitionTypeRepository, PostgresCompetitionTypeRepository>();
builder.Services.AddScoped<ICompetitionRepository, PostgresCompetitionRepository>();
builder.Services.AddScoped<IEventRepository, PostgresEventRepository>();
builder.Services.AddScoped<IClubMembershipRepository, PostgresClubMembershipRepository>();
builder.Services.AddScoped<IAdminRepository, MockAdminRepository>(); // MOCK
builder.Services.AddScoped<IEntityRepository, PostgresEntityRepository>();
builder.Services.AddScoped<IFieldRepository, PostgresFieldRepository>();
builder.Services.AddScoped<ILocationRepository, PostgresLocationRepository>();
builder.Services.AddScoped<IPenaltyRepository, MockPenaltyRepository>(); // MOCK
builder.Services.AddScoped<IRegistrationRepository, PostgresRegistrationRepository>();
builder.Services.AddScoped<IScoringSystemRepository, MockScoringSystemRepository>(); // MOCK
builder.Services.AddScoped<IParticipantRepository, PostgresParticipantRepository>();
builder.Services.AddScoped<IJudgeRepository, PostgresJudgeRepository>();
builder.Services.AddScoped<IMatchRepository, PostgresMatchRepository>();
builder.Services.AddScoped<IScoreRepository, PostgresScoreRepository>();
builder.Services.AddScoped<IScoreResultRepository, PostgresScoreResultRepository>();



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

if (app.Environment.IsDevelopment() || true)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
     
            // Incomment to seed database
            //var mockDatabaseManager = scope.ServiceProvider.GetRequiredService<MockDatabaseManager>();
            //DatabaseSeeder.SeedDatabase(context, mockDatabaseManager);

            // Apply pending migrations
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            // Log migration errors
            Console.WriteLine($"An error occurred during migration: {ex.Message}");
        }
    }
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
