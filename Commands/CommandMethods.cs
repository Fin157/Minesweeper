using Minesweeper.Gameplay;
using Minesweeper.Input;
using Minesweeper.Rendering;

namespace Minesweeper.Commands;

/// <summary>
/// Provides all the methods to be executed when the user runs commands
/// </summary>
internal static class CommandMethods
{
    public static void HelpCommand(Map map, string[] userInput)
    {
        // Show all the available command names, descriptions and parameters
        foreach (Command c in InputManager.CommandMappings)
            BufferedRenderer.AddToAdditional(new(c.ToString()));
    }

    public static void LeaveGameCommand(Map map, string[] userInput)
    {
        BufferedRenderer.AddToAdditional(new("Quitting game..."));

        // Stop the main loop
        Game.IsGameRunning = false;
    }

    public static void DigTileCommand(Map map, string[] userInput)
    {
        // Check if the input data is valid
        if (!Position.GetFromStrings(userInput[0], userInput[1], out Position pos) ||
            map[pos.x, pos.y].IsUncovered)
            return;

        BufferedRenderer.AddToAdditional(new("Digging up a tile on " + pos.ToString()));

        // Change state of the target tile
        map[pos.x, pos.y].IsUncovered = true;
    }

    public static void MarkTileCommand(Map map, string[] userInput)
    {
        // Check if the input data is valid
        if (userInput.Length < 3 ||
            !Position.GetFromStrings(userInput[0], userInput[1], out Position pos) ||
            map[pos.x, pos.y].IsUncovered)
            return;

        bool newMarked = false;
        try
        {
            newMarked = Convert.ToBoolean(userInput[2]);
        }
        catch { }
        string promptPrefix = newMarked ? "Marked" : "Unmarked";
        BufferedRenderer.AddToAdditional(new(promptPrefix + " a tile on " + pos.ToString()));

        // Change state of the target tile
        map[pos.x, pos.y].IsMarked = newMarked;
    }
}
