namespace Minesweeper.Gameplay;

/// <summary>
/// A base class for any tile on the map
/// </summary>
internal abstract class Tile
{
    /// <summary>
    /// Stores where this tile is on the map
    /// </summary>
    public Position Position { get; protected set; }
    /// <summary>
    /// Stores if this tile has a mark on it or not
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
            if (!isUncovered)
                return;
        }
    }
    /// <summary>
    /// Stores if this tile has been already dug up or not
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
            if (value) // Execute only if the new value is true
                OnUncover();
        }
    }

    protected bool isUncovered;
    protected bool isMarked;

    public Tile(int x, int y)
    {
        IsMarked = false;
        IsUncovered = false;
        Position = new(x, y);
    }

    /// <summary>
    /// Gets called when this tile instance gets uncovered
    /// </summary>
    protected abstract void OnUncover();
}