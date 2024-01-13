using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemoryRepository : IGameRepository
{
    List<Game> _games = new List<Game>(){
        new Game() { Id = 1, Name = "Super Mario Bros.", Genre = "Platformer", Price = 59.99m, ReleaseDate = new DateTime(1985, 9, 13),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/0/03/Super_Mario_Bros._box.png" },
        new Game() { Id = 2, Name = "The Legend of Zelda", Genre = "Action-adventure", Price = 59.99m, ReleaseDate = new DateTime(1986, 2, 21),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/4/41/Legend_of_zelda_cover_%28NES%29.jpg" },
        new Game() { Id = 3, Name = "Tetris", Genre = "Puzzle", Price = 49.99m, ReleaseDate = new DateTime(1984, 6, 6),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/7/7d/Tetris_NES_cover.jpg" },
        new Game() { Id = 4, Name = "Duck Hunt", Genre = "Light gun shooter", Price = 49.99m, ReleaseDate = new DateTime(1984, 4, 21),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/7/7b/Duck_Hunt.jpg" },
        new Game() { Id = 5, Name = "Super Mario Bros. 3", Genre = "Platformer", Price = 59.99m, ReleaseDate = new DateTime(1990, 2, 12),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/3/36/Super_Mario_Bros._3_coverart.png" },
        new Game() { Id = 6, Name = "Super Mario World", Genre = "Platformer", Price = 59.99m, ReleaseDate = new DateTime(1991, 8, 13),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/3/32/Super_Mario_World_Coverart.png" },
        new Game() { Id = 7, Name = "Super Mario Kart", Genre = "Racing", Price = 59.99m, ReleaseDate = new DateTime(1992, 8, 27),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/3/3c/Super_Mario_Kart_box.jpg" },
        new Game() { Id = 8, Name = "Donkey Kong Country", Genre = "Platformer", Price = 59.99m, ReleaseDate = new DateTime(1994, 11, 21),
            ImageUri = "https://upload.wikimedia.org/wikipedia/en/4/46/Donkey_Kong_Country.jpg" },
    };

    public Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return Task.Run(() => _games.AsEnumerable());
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        var game = _games.FirstOrDefault(g => g.Id == id);
        if (game == null)
        {
            return null;
        }

        return await Task.Run(() => game);
}

    public async Task<bool> DeleteAsync(int id)
    {
        var game = await GetByIdAsync(id);
        if (game == null)
        {
            return await Task.Run(()=> false);
        }

        _games.Remove(game);
        return await Task.Run(() => true);
    }

    public async Task<bool> UpdateAsync(int id, Game game)
    {
        var index = _games.FindIndex(g => g.Id == id);
        if (index == -1)
        {
            return await Task.Run(()=> false);
        }
        _games[index] = game;
        return await Task.Run(() => true);
    }

    public async Task<bool> CreateAsync(Game game)
    {
        var existingGame = await GetByIdAsync(game.Id);
        if (existingGame != null)
        {
            return await Task.Run(()=> false);
        }
        _games.Add(game);
        return await Task.Run(() => true);
    }
}