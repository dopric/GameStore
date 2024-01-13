using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllGamesAsync();
    Game? GetById(int id);
    bool Delete(int id);
    bool Update(int id, Game game);
    bool Create(Game game);
}