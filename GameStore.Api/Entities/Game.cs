namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string ImageUri { get; set; } = null!;
}