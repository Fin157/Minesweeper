using Minesweeper.Commands;

namespace Minesweeper.Input;

/// <summary>
/// A class that represents an AI user that plays the game
/// </summary>
internal class AIInputManager : IInputManager
{
    public Command? TakeInput(out string[] commandData)
    {
        commandData = Array.Empty<string>();
        return null;
    }
}
