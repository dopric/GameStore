using GameStore.Api;
using GameStore.Api.Repositories;
using GameStore.Api.Routes;

var builder = WebApplication.CreateBuilder(args);



var connString = builder.Configuration.GetConnectionString("GameStoreDb");
builder.Services.AddSqlServer<GameStoreDbContext>(connString);
//builder.Services.AddDbContext<GameStoreDbContext>(options => options.UseSqlServer(connString));

if (builder.Environment.IsDevelopment())
{
    // we could use a different repository here for testing purposes
    //builder.Services.AddScoped<IGameRepository, InMemoryRepository>();
}

builder.Services.AddScoped<IGameRepository, EFGameRepository>();
// builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

// for some unknown reason, git removed some files and folders from the project
// app.Services.InitializeDbAsync();


app.MapGameEndpoints();

app.MapGet("/", () => "Hello World!");



app.Run();
