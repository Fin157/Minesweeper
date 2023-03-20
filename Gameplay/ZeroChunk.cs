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
    private readonly HashSet<Tile> tiles;
    private bool isExposed = false;

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
        if (!isExposed)
        {
            isExposed = true;

            foreach (Tile t in tiles)
                t.IsUncovered = true;
        }
    }

    public override string ToString()
    {
        string result = "";

        foreach (Tile t in tiles)
        {
            result += t.Position.ToString();
        }
        result += "\n";

        return result;
    }
}
