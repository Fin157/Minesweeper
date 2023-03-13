namespace Minesweeper.Gameplay;

/// <summary>
/// A base class for any tile on the map
/// </summary>
internal abstract class Tile
{
    /// <summary>
    /// Where this tile is on the map
    /// </summary>
    public Position Position { get; protected set; }
    /// <summary>
    /// If this tile has a mark on it or not
    /// </summary>
    public bool IsMarked
    {
        get
        {
            return isMarked;
        }
        set
        {
            // Prevent ouside code from marking an uncovered tile
            if (isUncovered)
                return;

            isMarked = value;
        }
    }
    /// <summary>
    /// If this tile has been already dug up or not
    /// </summary>
    public bool IsUncovered
    {
        get
        {
            return isUncovered;
        }
        set
        {
            isUncovered = value;
            if (value) // Execute only if the new value is "true"
                OnUncover();
        }
    }

    protected bool isUncovered;
    protected bool isMarked;
    protected TileRenderer renderer;

    public Tile(int x, int y)
    {
        IsMarked = false;
        IsUncovered = false;
        Position = new(x, y);
        renderer = new();
    }

    public void Render(bool isDarker)
    {
        renderer.Render(this, isDarker);
    }

    protected abstract void OnUncover();
}