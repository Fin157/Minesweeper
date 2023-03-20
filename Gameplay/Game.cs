using Minesweeper.Commands;
using Minesweeper.Input;
using Minesweeper.Rendering;

namespace Minesweeper.Gameplay;

internal static class Game
{
    private const int MAP_SIZE_X = 6;
    private const int MAP_SIZE_Y = 12;
    private const int MINE_COUNT = 10;

    /// <summary>
    /// Controls if this game class' main loop is running
    /// </summary>
    public static bool IsLoopAlive { get; set; }
    public static bool IsDebugOn { get; set; } = false;

    public static readonly Map map = Map.GenerateMap(MAP_SIZE_X, MAP_SIZE_Y, MINE_COUNT);

    public static void StartGame()
    {
        GameLoop(map);
    }

    private static void GameLoop(Map map)
    {
        // Start the loop
        IsLoopAlive = true;

        // Render the empty field
        Render();

        while (IsGameRunning())
        {
            // Gather user input
            Command? command = InputManager.TakeInput(out string[] commandData);

            // Process the input
            ProcessInput(map, command, commandData);

            // Render the changes
            Render();
        }

        GameEnd();
    }

    private static void ProcessInput(Map map, Command? command, string[] commandData)
    {
        // Execute the chosen command on the specified tile (if it is not null)
        // and return the result of the command's execute method
        command?.Execute(map, commandData);
    }

    private static void Render()
    {
        // Fill the main buffer with tile textures
        map.RenderMap(IsDebugOn);

        BufferedRenderer.Render();
    }

    /// <summary>
    /// Checks if all the boolean values game state depends on and outputs the result of the calculation
    /// </summary>
    /// <returns>True if the game should be running, otherwise false</returns>
    public static bool IsGameRunning() => IsLoopAlive && !map.IsMapClear;

    private static void GameEnd()
    {
        // The game didn't end violently (the player won)
        if (map.IsMapClear)
            BufferedRenderer.AddToAdditional("You win!");

        Render();
    }
}