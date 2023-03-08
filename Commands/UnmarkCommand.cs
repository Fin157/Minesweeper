using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal class UnmarkCommand : ICommand
{
    public string Code { get; init; }

    public UnmarkCommand(string code)
    {
        Code = code;
    }

    public void Execute(Map map, string[] userInput)
    {
        
    }
}
