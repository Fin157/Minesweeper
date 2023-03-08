using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal interface ICommand
{
    public string Code { get; init; }
    public abstract void Execute(Map map, string[] userInput);

    protected bool GetPosition(string positionText, out int? position)
    {
        position = !int.TryParse(positionText, out int pos) ? pos : null;
        return position != null;
    }
}
