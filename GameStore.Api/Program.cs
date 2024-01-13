using GameStore.Api;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;
using GameStore.Api.Routes;
using GameStore.Api.Services;
using GameStore.Api.Services.Contracts;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);



var connString = builder.Configuration.GetConnectionString("GameStoreDb");
builder.Services.AddSqlServer<GameStoreDbContext>(connString);
//builder.Services.AddDbContext<GameStoreDbContext>(options => options.UseSqlServer(connString));

builder.Services.AddScoped<IGameRepository, InMemoryRepository>();

var app = builder.Build();
app.MapGameEndpoints();

app.MapGet("/", () => "Hello World!");



app.Run();
