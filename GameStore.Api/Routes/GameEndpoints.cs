// GameEndpoints.cs
using GameStore.Api.Entities;
using GameStore.Api.Services.Contracts;

namespace GameStore.Api.Routes;

/// <summary>
/// Provides static methods to map game-related endpoints for a web API.
/// </summary>
public static class GameEndpoints
{
    private static IGameService _gameService;

    /// <summary>
    /// Initializes the game service.
    /// </summary>
    /// <param name="gameService">The game service to initialize.</param>
    internal static void Initialize(IGameService gameService)
    {
        _gameService = gameService;
    }

    /// <summary>
    /// Maps the endpoints for game related operations.
    /// </summary>
    /// <param name="routes">The <see cref="IEndpointRouteBuilder"/> object used for endpoint mapping.</param>
    public static void MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGetGameEndpoint();
        routes.MapGetGameByIdEndpoint();
        routes.MapPostGameEndpoint();
        routes.MapPutGameEndpoint();
        routes.MapDeleteGameEndpoint();
    }

    /// <summary>
    /// Maps the GET game endpoint to the specified route.
    /// </summary>
    /// <param name="routes">The endpoint route builder.</param>
    private static void MapGetGameEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/", () => _gameService.GetAllGames());
    }

    /// <summary>
    /// Maps the endpoint for getting a game by its ID.
    /// </summary>
    /// <param name="routes">The <see cref="IEndpointRouteBuilder"/> to map the endpoint to.</param>
    private static void MapGetGameByIdEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/{id}", (int id) => _gameService.GetAllGames().Result.FirstOrDefault() is { } game ? Results.Ok(game) : Results.NotFound())
            .WithName(Constants.GetGameById);
    }

    /// <summary>
    /// Maps the POST / endpoint to the provided route builder.
    /// </summary>
    /// <param name="routes">The endpoint route builder.</param>
    private static void MapPostGameEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", (Game game) =>
        {
            _gameService.AddGame(game);
            return Results.Created($"/api/games/{game.Id}", game);
        });
    }

    /// <summary>
    /// Maps the PUT game endpoint to a specific route and handler.
    /// </summary>
    /// <param name="routes">The instance of <see cref="IEndpointRouteBuilder"/> used to configure the endpoint routes.</param>
    private static void MapPutGameEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/{id}", (int id, Game game) =>
        {
            if (_gameService.UpdateGame(id, game))
            {
                return Results.Ok(game);
            }
            return Results.NotFound();
        });
    }

    /// <summary>
    /// Maps the DELETE /{id} endpoint to delete a game with the specified ID.
    /// </summary>
    /// <param name="routes">The endpoint route builder.</param>
    private static void MapDeleteGameEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapDelete("/{id}", (int id) =>
        {
            if (_gameService.DeleteGame(id))
            {
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}