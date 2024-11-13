using competex_backend.API.Controllers;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.DAL.Interfaces;
using competex_backend;
using competex_backend.BLL.Services;
using competex_backend.BLL.Interfaces;
using competex_backend.API.DTOs;
using competex_backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MockDatabaseManager>();

# region IGenericRepository
//Registers IGenericRepository<T> with specific implementations (MockMemberRepository, MockClubRepository, etc.) for each model type.
builder.Services.AddScoped<IGenericRepository<Member>, MockMemberRepository>();
builder.Services.AddScoped<IGenericRepository<Club>, MockClubRepository>();
builder.Services.AddScoped<IGenericRepository<Round>, MockRoundRepository>();
builder.Services.AddScoped<IGenericRepository<SportType>, MockSportTypeRepository>();
builder.Services.AddScoped<IGenericRepository<CompetitionType>, MockCompetitionTypeRepository>();
builder.Services.AddScoped<IGenericRepository<Competition>, MockCompetitionRepository>();
builder.Services.AddScoped<IGenericRepository<Event>, MockEventRepository>();
builder.Services.AddScoped<IGenericRepository<ClubMembership>, MockClubMembershipRepository>();
builder.Services.AddScoped<IGenericRepository<Admin>, MockAdminRepository>();
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
# endregion

#region Service DTO Mappings
// Registers services with DTO mappings for each model type.
builder.Services.AddScoped<IMemberRepository, MockMemberRepository>();
builder.Services.AddScoped<IClubRepository, MockClubRepository>();
builder.Services.AddScoped<IRoundRepository, MockRoundRepository>();
builder.Services.AddScoped<ISportTypeRepository, MockSportTypeRepository>();
builder.Services.AddScoped<ICompetitionTypeRepository, MockCompetitionTypeRepository>();
builder.Services.AddScoped<ICompetitionRepository, MockCompetitionRepository>();
builder.Services.AddScoped<IEventRepository, MockEventRepository>();
builder.Services.AddScoped<IClubMembershipRepository, MockClubMembershipRepository>();
builder.Services.AddScoped<IAdminRepository, MockAdminRepository>();
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

# endregion

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
