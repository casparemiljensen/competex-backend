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

builder.Services.AddScoped<IGenericRepository<Member>, MockMemberRepository>();
builder.Services.AddScoped<IGenericRepository<Club>, MockClubRepository>();
builder.Services.AddScoped<IGenericRepository<Round>, MockRoundRepository>();
builder.Services.AddSingleton<MockDatabaseManager>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IRoundService, RoundService>();
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
