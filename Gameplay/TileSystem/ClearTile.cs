﻿namespace Minesweeper.Gameplay.TileSystem;

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
        set
        {
            if (!wasMinesAroundSet)
            {
                wasMinesAroundSet = true;
                minesAround = value;
            }
        }
    }
    public ZeroChunk? Chunk { get; set; }

    /// <summary>
    /// Stores how many mines this tile neighbours
    /// </summary>
    private int minesAround;
    private bool wasMinesAroundSet = false;

    public ClearTile(int x, int y, bool isDarker, int minesAround) : base(x, y, isDarker)
    {
        this.minesAround = minesAround;
    }

    public ClearTile(int x, int y, bool isDarker) : base(x, y, isDarker) { }

    protected override void OnUncover()
    {
        // Do nothing, this tile is inert
    }
}
