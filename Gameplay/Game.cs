using Minesweeper.Commands;
using Minesweeper.Input;
using Minesweeper.Rendering;

namespace Minesweeper.Gameplay;

internal class Game
{
    /// <summary>
    /// Controls if this game class' main loop is running
    /// </summary>
    public bool IsLoopAlive { get; set; }
    public bool IsDebugOn { get; set; } = false;

    private readonly IInputManager player;
    private readonly Map map;
    private readonly bool renderChanges;

    public Game(IInputManager player, int mapSizeX, int mapSizeY, int mineCount, bool renderChanges)
    {
        this.player = player;
        map = Map.GenerateMap(this, mapSizeX, mapSizeY, mineCount);
        this.renderChanges = renderChanges;
        GameLoop(map);
    }

    private void GameLoop(Map map)
    {
        // Start the loop
        IsLoopAlive = true;

        // Render the empty field
        Render();

        while (IsGameRunning())
        {
            // Gather user input
            Command? command = player.TakeInput(out string[] commandData);

            // Process the input
            ProcessInput(map, command, commandData);

            // Render the changes
            Render();
        }

        GameEnd();
    }

    private void ProcessInput(Map map, Command? command, string[] commandData)
    {
        // Execute the chosen command on the specified tile (if it is not null)
        // and return the result of the command's execute method
        command?.Execute(map, commandData);
    }

    private void Render()
    {
        if (!renderChanges)
            return;

        // Fill the main buffer with tile textures
        map.RenderMap(IsDebugOn);

        BufferedRenderer.Render();
    }

    /// <summary>
    /// Checks if all the boolean values game state depends on and outputs the result of the calculation
    /// </summary>
    /// <returns>True if the game should be running, otherwise false</returns>
    public bool IsGameRunning() => IsLoopAlive && !map.IsMapClear;

    private void GameEnd()
    {
        // The game didn't end violently (the player won)
        if (map.IsMapClear)
            BufferedRenderer.AddToAdditional("You win!");

        Render();
    }
}