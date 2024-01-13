using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Repositories;
using GameStore.Api.Routes;

var builder = WebApplication.CreateBuilder(args);



var connString = builder.Configuration.GetConnectionString("GameStoreDb");
builder.Services.AddSqlServer<GameStoreDbContext>(connString);
//builder.Services.AddDbContext<GameStoreDbContext>(options => options.UseSqlServer(connString));

builder.Services.AddScoped<IGameRepository, InMemoryRepository>();


var app = builder.Build();
app.Services.InitializeDb();


app.MapGameEndpoints();

app.MapGet("/", () => "Hello World!");



app.Run();
