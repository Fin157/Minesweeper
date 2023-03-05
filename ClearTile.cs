namespace Minesweeper;

/// <summary>
/// A class representing a tile where no mine is hidden
/// </summary>
internal class ClearTile : Tile
{
    /// <summary>
    /// Returns how many mines this tile neighbours. 
    /// Returns the real value if the tile has already been uncovered, otherwise returns -1.
    /// </summary>
    public int MinesAround
    {
        get
        {
            return isUncovered ? minesAround : -1;
        }
    }

    /// <summary>
    /// Stores how many mines this tile neighbours
    /// </summary>
    private int minesAround;

    public ClearTile(int x, int y, int minesAround) : base(x, y)
    {
        this.minesAround = minesAround;
    }

    protected override void OnUncover()
    {
        base.OnUncover();
    }
}
