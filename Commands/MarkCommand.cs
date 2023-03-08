using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal class MarkCommand : ICommand
{
    public string Code { get; init; }

    public MarkCommand(string code)
    {
        Code = code;
    }

    public void Execute(Map map, string[] userInput)
    {
        
    }
}
