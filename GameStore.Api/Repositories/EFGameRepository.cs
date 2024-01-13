using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EFGameRepository : IGameRepository
{
    private readonly GameStoreDbContext _dbContext;

    public EFGameRepository(GameStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return _dbContext.Games.AsNoTracking().ToList();
    }

    public Game? GetById(int id)
    {
        return _dbContext.Games.FirstOrDefault(g => g.Id == id);
    }

    public bool Delete(int id)
    {
        if (_dbContext.Games.Any(g => g.Id == id))
        {
            _dbContext.Games.Remove(_dbContext.Games.First(g => g.Id == id));
            _dbContext.SaveChanges();
            return true;
        }

        return false;
    }

    public bool Update(int id, Game game)
    {
        _dbContext.Games.Update(game);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Create(Game game)
    {
        _dbContext.Add<Game>(game);
        _dbContext.SaveChanges();
        return true;
    }
}