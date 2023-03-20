using Minesweeper.Rendering;

namespace Minesweeper.Gameplay.TileSystem;

/// <summary>
/// A class representing a tile where a mine is hidden
/// </summary>
internal class MineTile : Tile
{
    public MineTile(int x, int y, bool isDarker, Map parent) : base(x, y, isDarker, parent) { }

    protected override void OnUncover()
    {
        // This is game over, the player tried to dig up a mine and exploded
        // Let the game loop know

        BufferedRenderer.AddToAdditional("Oh no! You exploded!");

        Game.IsLoopAlive = false;
    }
}
