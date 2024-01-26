# Versioning 
```http request
GET /api/games/1?api-version=1.0
```
## Using version specific DTOs
```c#
public class GameDtoV1
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    // new property
    public string? Publisher { get; set; }
}
```
## Add URL versioning
```c#
var v1GRoup = routes.MapGroup("api/v1");
.WithParameterValidaton();
v1GRoup.MapGet("/games", async (IGameRepository repository) =>
{
    return Results.Ok(await repository.GetGamesAsync());
}).WithName("GetGamesV1");

var v2Group = routes.MapGroup("api/v2");
```

## Using AspNetCore.Versioning package
```c#
// add package AspNetCore.Versioning.Http
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});

var group =routes.NewVersionedApi()
    .MapGroup("/games")
    .HasApiVersion(new ApiVersion(1, 0))
    .HasApiVersion(2.0);

group.MapGet("/", async (IGameRepository repository) =>{})
    .MapApiVersion(1.0);

```

## Using Query String versioning
```c#
// HTTP request
GET /api/games?api-version=1.0
```

## Specify default API version
```c#
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});
```