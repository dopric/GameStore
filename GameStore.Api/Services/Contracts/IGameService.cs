using GameStore.Api.Entities;

namespace GameStore.Api.Services.Contracts;

public interface IGameService
{
    public Task<IEnumerable<Game>> GetAllGames();
    void AddGame(Game game);
    bool UpdateGame(int id, Game game);
    bool DeleteGame(int id);
    Game? GetGameById(int id);
}