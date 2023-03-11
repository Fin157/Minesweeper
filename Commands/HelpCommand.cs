using Minesweeper.Gameplay;
using Minesweeper.Input;

namespace Minesweeper.Commands;

internal class HelpCommand : ICommand
{
    public void Execute(Map map, string[] userInput)
    {
        // Show all the available commands and their descriptions
        foreach (CommandData cd in InputManager.CommandMappings.Keys)
            BufferedRenderer.AddLine(cd.ToString());
    }
}
