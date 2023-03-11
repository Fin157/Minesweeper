using Minesweeper.Commands;

namespace Minesweeper.Input;

/// <summary>
/// A class used for gathering keypress input from the user
/// </summary>
internal static class InputManager
{
    public static readonly Dictionary<CommandData, ICommand> CommandMappings = new()
    {
        [new("HELP", "Lists all available commands.")] = new HelpCommand(),
        [new("DIG", "Digs up the specified covered tile.", "tile X", "tile Y")] = new DigCommand(),
        [new("MARK", "Marks the specified uncovered tile.", "tile X", "tile Y")] = new MarkCommand(),
        [new("UNMK", "Unmarks the specified marked uncovered tile.", "tile X", "tile Y")] = new UnmarkCommand(),
        [new("QUIT", "Quits game.")] = new LeaveGameCommand()
    };

    public static bool TakeInput(out ICommand? command, out string[] commandData)
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
        foreach (CommandData cd in CommandMappings.Keys)
        {
            if (splitInput[0] == cd.Code)
            {
                command = CommandMappings[cd];
                break;
            }
        }

        splitInput.RemoveAt(0); // Remove the command type itself and keep just the command data
        commandData = splitInput.ToArray();

        return true;
    }
}