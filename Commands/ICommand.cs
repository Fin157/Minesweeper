using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal interface ICommand
{
    public abstract void Execute(Map map, string[] userInput);

    protected bool GetPosition(string positionText, out int? position)
    {
        position = !int.TryParse(positionText, out int pos) ? pos : null;
        return position != null;
    }
}

internal readonly struct CommandData
{
    public string Code { get; init; }
    public string Description { get; init; }
    public string[] Parameters { get; init; }

    public CommandData(string code, string description, params string[] parameters)
    {
        Code = code;
        Description = description;
        Parameters = parameters;
    }

    public override string ToString()
    {
        string result = Code;

        foreach (string p in Parameters)
            result += $" [{p}]";

        result += " " + Description;

        return result;
    }
}