using Minesweeper.Gameplay.TileSystem;

namespace Minesweeper.Gameplay;

internal class ZeroChunk
{
    /// <summary>
    /// How many tiles fall into this zero chunk
    /// </summary>
    public int Size { get => tiles.Count; }

    /// <summary>
    /// All the tile positions
    /// </summary>
    private readonly List<Tile> tiles;

    public ZeroChunk()
    {
        tiles = new();
    }

    public void AddTile(Tile t)
    {
        tiles.Add(t);
        (t as ClearTile).Chunk = this;
    }

    public void Expose()
    {
        foreach (Tile t in tiles)
            t.IsUncovered = true;
    }
}
