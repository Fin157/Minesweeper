using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

/// <summary>
/// A command for the user to interact with the tiles
/// </summary>
internal class Command
{
    public delegate void ExecuteCommand(Map map, string[] userInput);
    /// <summary>
    /// The name of the command (the keyword used to access it)
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// A brief description of the command
    /// </summary>
    public string Description { get; init; }
    /// <summary>
    /// What parameters this command takes in
    /// </summary>
    public string[] Parameters { get; init; }
    /// <summary>
    /// What method this command executes when ran
    /// </summary>
    public ExecuteCommand Execute { get; init; }

    public Command(string code, string description, ExecuteCommand execute, params string[] parameters)
    {
        Name = code;
        Description = description;
        Execute = execute;
        Parameters = parameters;
    }

    public override string ToString()
    {
        string result = Name;

        foreach (string p in Parameters)
            result += $" [{p}]";

        result += ": " + Description;

        return result;
    }
}