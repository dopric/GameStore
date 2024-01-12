using GameStore.Api.Entities;
using GameStore.Api.Services.Contracts;

namespace GameStore.Api.Services;

public class InMemoryGameService : IGameService
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
    
    public async Task<IEnumerable<Game>> GetAllGames()
    {
        return await Task.Run(() => _games.AsEnumerable()) ;
    }

    public void AddGame(Game game)
    {
        _games.Add(game);
    }

    public bool UpdateGame(int id, Game game)
    {
        var index = _games.FindIndex(g => g.Id == id);
        if (index == -1)
        {
            return false;
        }
        _games[index] = game;
        return true;
    }
    
    public bool DeleteGame(int id)
    {
        var index = _games.FindIndex(g => g.Id == id);
        if (index == -1)
        {
           return false;
        }
        _games.RemoveAt(index);
        return true;
    }

    public Game? GetGameById(int id)
    {
        int index = _games.FindIndex(p=> p.Id == id);
        if (index == -1)
        {
            return null;
        }
        return _games[index];
    }
}