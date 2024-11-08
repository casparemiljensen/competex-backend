using competex_backend.API.Controllers;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.DAL.Interfaces;
using competex_backend;
using competex_backend.BLL.Services;
using competex_backend.BLL.Interfaces;
using competex_backend.API.DTOs;
using competex_backend.Models;
using Microsoft.Win32;

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
builder.Services.AddScoped<IGenericRepository<Competition>, MockCompetitionRepository>();
#endregion


# region Services 
// Registers services specific to each model's business logic (such as IMemberService for Member, IClubService for Club, etc.).
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IRoundService, RoundService>();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
# endregion

# region IGenericService
// Register services with DTO mappings for IGenericService<TDto>
// Registers a GenericService to handle operations on DTOs (Data Transfer Objects) for each model type.
builder.Services.AddScoped<IGenericService<MemberDTO>, GenericService<Member, MemberDTO>>();
builder.Services.AddScoped<IGenericService<ClubDTO>, GenericService<Club, ClubDTO>>();
builder.Services.AddScoped<IGenericService<RoundDTO>, GenericService<Round, RoundDTO>>();
builder.Services.AddScoped<IGenericService<CompetitionDTO>, GenericService<Competition, CompetitionDTO>>();
# endregion

//builder.Services.AddScoped<IClubRepository, MockClubRepository>();
//builder.Services.AddScoped<IMemberRepository, MockMemberRepository>();
//builder.Services.AddScoped<IClubMemberRepository, MockClubMemberRepository>();

// Register MockDatabaseManager as a singleton
//builder.Services.AddSingleton<IDatabaseManager, MockDatabaseManager>();

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
