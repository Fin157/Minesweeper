using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal class LeaveGameCommand : ICommand
{
    public void Execute(Map map, string[] userInput)
    {
        // Leave game
        Program.IsGameRunning = false;
    }
}
