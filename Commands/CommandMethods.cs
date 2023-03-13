using Minesweeper.Gameplay;
using Minesweeper.Input;

namespace Minesweeper.Commands;

/// <summary>
/// Provides all the methods to be executed when the user runs commands
/// </summary>
internal static class CommandMethods
{
    public static bool HelpCommand(Map map, string[] userInput)
    {
        // Show all the available command names, descriptions and parameters
        foreach (Command c in InputManager.CommandMappings)
            Console.WriteLine(c.ToString());

        return false;
    }

    public static bool LeaveGameCommand(Map map, string[] userInput)
    {
        // Stop the main loop
        Program.IsGameRunning = false;

        return false;
    }

    public static bool DigTileCommand(Map map, string[] userInput)
    {
        if (!Position.GetFromStrings(userInput[0], userInput[1], out Position pos) ||
            map[pos.x, pos.y].IsUncovered)
            return true;

        map[pos.x, pos.y].IsUncovered = true;

        return true;
    }

    public static bool MarkTileCommand(Map map, string[] userInput)
    {
        if (userInput.Length < 3 ||
            !Position.GetFromStrings(userInput[0], userInput[1], out Position pos) ||
            map[pos.x, pos.y].IsUncovered)
            return true;

        map[pos.x, pos.y].IsMarked = Convert.ToBoolean(userInput[2]);

        return true;
    }
}
