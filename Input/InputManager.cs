using Minesweeper.Commands;

namespace Minesweeper.Input;

/// <summary>
/// A class used for gathering keypress input from the user
/// </summary>
internal static class InputManager
{
    /// <summary>
    /// Remembers all the possible commands
    /// </summary>
    public static readonly List<Command> CommandMappings = new()
    {
        new("HELP", "Lists all available commands.", CommandMethods.HelpCommand),
        new("QUIT", "Quits game.", CommandMethods.LeaveGameCommand),
        new("DIG", "Digs up the specified covered tile.", CommandMethods.DigTileCommand, "tile X (integer)", "tile Y (integer)"),
        new("MARK", "Marks or unmarks the specified uncovered tile.", CommandMethods.MarkTileCommand, "tile X (integer)", "tile Y (integer)", "marked (true/false)")
    };

    /// <summary>
    /// Takes an input from the user and returns it as raw string data and a command to be executed
    /// </summary>
    /// <param name="commandData">An out parameter for the command data</param>
    /// <returns>A command to be executed on the command data (can be null)</returns>
    public static Command? TakeInput(out string[] commandData)
    {
        commandData = Array.Empty<string>();

        Console.Write("/");

        // Prevent receiving a null input
        string? input = Console.ReadLine();

        if (input == null)
            return null;

        List<string> splitInput = new(input.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries));

        if (splitInput.Count == 0)
            return null;

        // Find out which command the user chose
        Command? command = null;
        foreach (Command c in CommandMappings)
        {
            if (splitInput[0] == c.Name)
            {
                command = c;
                break;
            }
        }

        splitInput.RemoveAt(0); // Remove the command type itself from the string array and keep just the command data
        commandData = splitInput.ToArray();

        return command;
    }
}