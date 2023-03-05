namespace Minesweeper;

/// <summary>
/// A class representing a tile where a mine is hidden
/// </summary>
internal class MineTile : Tile
{
    public MineTile(int x, int y) : base(x, y) { }

    protected override void OnUncover()
    {
        base.OnUncover();

        // This is game over, the player tried to dig up a mine and exploded
        // Let the game loop know
    }
}
