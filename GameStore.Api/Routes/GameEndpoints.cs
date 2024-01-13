// GameEndpoints.cs
using GameStore.Api.Entities;
using GameStore.Api.Repositories;
using GameStore.Api.Services.Contracts;

namespace GameStore.Api.Routes;

/// <summary>
/// Provides static methods to map game-related endpoints for a web API.
/// </summary>
public static class GameEndpoints
{
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
        routes.MapGet("/api/games/", (IGameRepository repository) => repository.GetAllGamesAsync());
    }

    /// <summary>
    /// Maps the endpoint for getting a game by its ID.
    /// </summary>
    /// <param name="routes">The <see cref="IEndpointRouteBuilder"/> to map the endpoint to.</param>
    private static void MapGetGameByIdEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/games/{id}", (IGameRepository repository, int id) => repository.GetAllGamesAsync().Result.FirstOrDefault() is { } game ? Results.Ok(game) : Results.NotFound())
            .WithName(Constants.GetGameById);
    }

    /// <summary>
    /// Maps the POST / endpoint to the provided route builder.
    /// </summary>
    /// <param name="routes">The endpoint route builder.</param>
    private static void MapPostGameEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/games/", (IGameRepository repository, Game game) =>
        {
            repository.Create(game);
            return Results.Created($"/api/games/{game.Id}", game);
        });
    }

    /// <summary>
    /// Maps the PUT game endpoint to a specific route and handler.
    /// </summary>
    /// <param name="routes">The instance of <see cref="IEndpointRouteBuilder"/> used to configure the endpoint routes.</param>
    private static void MapPutGameEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/api/games/{id}", (IGameRepository repository, int id, Game game) =>
        {
            if (repository.Update(id, game))
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
        routes.MapDelete("/api/games/{id}", (IGameRepository repository, int id) =>
        {
            if (repository.Delete(id))
            {
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}