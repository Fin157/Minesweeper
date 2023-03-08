using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal class DigCommand : ICommand
{
    public string Code { get; init; }

    public DigCommand(string code)
    {
        Code = code;
    }

    public void Execute(Map map, string[] userInput)
    {
        //map[pos.x, pos.y].IsUncovered = true;
    }
}
