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
        return await _dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (await _dbContext.Games.AnyAsync(g => g.Id == id))
        {
             _dbContext.Games.Remove(_dbContext.Games.First(g => g.Id == id));
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateAsync(int id, Game game)
    {
         _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CreateAsync(Game game)
    {
        await _dbContext.AddAsync<Game>(game);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}