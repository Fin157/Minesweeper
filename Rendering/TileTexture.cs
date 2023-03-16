using Minesweeper.Gameplay.TileSystem;

namespace Minesweeper.Rendering;

/// <summary>
/// A class which is used by individual tiles to calculate how they should look
/// </summary>
internal class TileTexture
{
    /// <summary>
    /// The tile whose data this instance uses to calculate how the texture looks
    /// </summary>
    private readonly Tile owner;
    /// <summary>
    /// Controls if the tile this renderer belongs to is darker or not
    /// </summary>
    private readonly bool isDarker;
    /// <summary>
    /// Stores all the colours tiles can use under a string key in both light and dark variants
    /// </summary>
    private readonly Dictionary<string, TileColour> tileColours = new()
    {
        ["regular"] = new(ConsoleColor.Green, ConsoleColor.DarkGreen),
        ["marked"] = new(ConsoleColor.Red, ConsoleColor.DarkRed),
        ["uncovered"] = new(ConsoleColor.Blue, ConsoleColor.DarkBlue),
        ["mine"] = new(ConsoleColor.Yellow, ConsoleColor.DarkYellow),
        ["default"] = new(ConsoleColor.Gray, ConsoleColor.Black)
    };

    public TileTexture(Tile owner, bool isDarker)
    {
        this.owner = owner;
        this.isDarker = isDarker;
    }

    /// <summary>
    /// Returns a render line that represents how the owner tile physically looks when rendered
    /// </summary>
    public RenderLine Texture
    {
        get
        {
            string renderText = "  ";
            TileColour tileColour;

            if (owner.IsUncovered)
            {
                switch (owner.GetType().Name)
                {
                    case nameof(ClearTile):
                        tileColour = tileColours["uncovered"];
                        ClearTile t = (ClearTile)owner;
                        if (t.MinesAround != 0)
                            renderText = t.MinesAround.ToString("00");
                        break;
                    case nameof(MineTile):
                        tileColour = tileColours["mine"];
                        break;
                    default:
                        tileColour = tileColours["default"];
                        break;
                }
            }
            else
                tileColour = owner.IsMarked ? tileColours["marked"] : tileColours["regular"];

            ConsoleColor renderColour = isDarker ? tileColour.Dark : tileColour.Light;
            return new(renderText, Enumerable.Repeat(renderColour, renderText.Length).ToArray());
        }
    }
}

/// <summary>
/// A struct representing one colour the tile can have
/// </summary>
internal readonly struct TileColour
{
    /// <summary>
    /// The lighter variant of the colour
    /// </summary>
    public ConsoleColor Light { get; init; }
    /// <summary>
    /// The darker variant of the colour
    /// </summary>
    public ConsoleColor Dark { get; init; }

    public TileColour(ConsoleColor light, ConsoleColor dark)
    {
        Light = light;
        Dark = dark;
    }
}