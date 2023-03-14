using Minesweeper.Rendering;

namespace Minesweeper.Gameplay.TileSystem;

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
    /// <summary>
    /// Returns a two chars long string to be rendered as the texture of this tile
    /// </summary>
    public RenderLine Texture { get => renderer.Texture; }

    protected bool isUncovered;
    protected bool isMarked;
    protected TileTexture renderer;

    public Tile(int x, int y, bool isDarker)
    {
        IsMarked = false;
        IsUncovered = false;
        Position = new(x, y);
        renderer = new(this, isDarker);
    }

    /// <summary>
    /// Gets called when this tile gets dug up
    /// </summary>
    protected abstract void OnUncover();
}