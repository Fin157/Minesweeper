namespace Minesweeper.Gameplay;

internal class TileRenderer
{
    private readonly Dictionary<string, TileColour> tileColours = new()
    {
        ["regular"] = new(ConsoleColor.Green, ConsoleColor.DarkGreen),
        ["marked"] = new(ConsoleColor.Red, ConsoleColor.DarkRed),
        ["uncovered"] = new(ConsoleColor.Blue, ConsoleColor.DarkBlue)
    };

    public void Render(Tile owner, bool isDarker)
    {
        string renderText = "  ";
        TileColour renderColour;

        if (owner.IsUncovered)
        {
            renderColour = tileColours["uncovered"];

            renderText = owner.GetType().Name switch
            {
                nameof(ClearTile) => (owner as ClearTile).MinesAround.ToString("00"),
                _ => "  "
            };
        }
        else
            renderColour = owner.IsMarked ? tileColours["marked"] : tileColours["regular"];

        Console.BackgroundColor = isDarker ? renderColour.Dark : renderColour.Light;
        Console.Write(renderText);
    }
}

internal readonly struct TileColour
{
    public ConsoleColor Light { get; init; }
    public ConsoleColor Dark { get; init; }

    public TileColour(ConsoleColor light, ConsoleColor dark)
    {
        Light = light;
        Dark = dark;
    }
}