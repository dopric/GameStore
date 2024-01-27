using GameStore.Api.Entities;

namespace GameStore.Api.Entities;

public static class GameExtensions
{
    public static GameDto AsDto(this Game game)
    {
        return new GameDto(game.Id, game.Name, game.Genre, game.Price, game.ReleaseDate, game.ImageUri);
    }
    
    public static Game AsEntity(this GameDto game)
    {
        return new Game
        {
            Name = game.Name,
            Genre = game.Genre,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
            ImageUri = game.ImageUri
        };
    }
}