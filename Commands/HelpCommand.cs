using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal class HelpCommand : ICommand
{
    public string Code { get; init; }

    public HelpCommand(string code)
    {
        Code = code;
    }

    public void Execute(Map map, string[] userInput)
    {
        // Show all the available commands and their descriptions
    }
}
