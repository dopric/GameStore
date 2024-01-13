using GameStore.Api;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;
using GameStore.Api.Routes;
using GameStore.Api.Services;
using GameStore.Api.Services.Contracts;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGameRepository, InMemoryRepository>();


var app = builder.Build();
app.MapGameEndpoints();

app.MapGet("/", () => "Hello World!");



app.Run();
