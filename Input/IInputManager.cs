using Minesweeper.Commands;

namespace Minesweeper.Input;

/// <summary>
/// A base interface for any input manager class
/// </summary>
internal interface IInputManager
{
    /// <summary>
    /// Gets called to retrieve some form of input for outside use (f. e. user input)
    /// </summary>
    /// <param name="commandData">Data for the command to be executed as an array of raw strings</param>
    /// <returns>A command to be executed (possibly null)</returns>
    public abstract Command? TakeInput(out string[] commandData);
}