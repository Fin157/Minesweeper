using Minesweeper.Commands;

namespace Minesweeper.Input;

/// <summary>
/// A class used for gathering keypress input from the user
/// </summary>
internal static class InputManager
{
    private static readonly List<ICommand> commandMappings = new()
    {
        new HelpCommand("HELP"),
        new DigCommand("DIG"),
        new MarkCommand("MARK"),
        new UnmarkCommand("UNMK")
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
        foreach (ICommand c in commandMappings)
        {
            if (splitInput[0] == c.Code)
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