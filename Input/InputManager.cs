using Minesweeper.Commands;

namespace Minesweeper.Input;

/// <summary>
/// A class used for gathering keypress input from the user
/// </summary>
internal static class InputManager
{
    public static readonly List<Command> CommandMappings = new()
    {
        new("HELP", "Lists all available commands.", CommandMethods.HelpCommand),
        new("QUIT", "Quits game.", CommandMethods.LeaveGameCommand),
        new("DIG", "Digs up the specified covered tile.", CommandMethods.DigTileCommand, "tile X (integer)", "tile Y (integer)"),
        new("MARK", "Marks or unmarks the specified uncovered tile.", CommandMethods.MarkTileCommand, "tile X (integer)", "tile Y (integer)", "marked (true/false)")
    };

    public static bool TakeInput(out Command? command, out string[] commandData)
    {
        // Syntax of one user input line: /(type of command) [(tile x)] [(tile y)]
        command = null;
        commandData = Array.Empty<string>();

        // Prevent receiving a null input
        string? input;
        do
        {
            Console.Write("/");
            input = Console.ReadLine();
        } while (input == null);

        List<string> splitInput = new(input.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        if (splitInput.Count == 0)
            return false;

        // Find out which command the user chose
        foreach (Command c in CommandMappings)
        {
            if (splitInput[0] == c.Name)
            {
                command = c;
                break;
            }
        }

        splitInput.RemoveAt(0); // Remove the command type itself and keep just the command data
        commandData = splitInput.ToArray();

        return true;
    }
}