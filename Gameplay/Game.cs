using Minesweeper.Commands;
using Minesweeper.Input;
using Minesweeper.Rendering;

namespace Minesweeper.Gameplay;

internal static class Game
{
    private const int MAP_SIZE_X = 5;
    private const int MAP_SIZE_Y = 5;
    private const int MINE_COUNT = 1;

    public static bool IsGameRunning { get; set; }

    public static readonly Map map = Map.GenerateMap(MAP_SIZE_X, MAP_SIZE_Y, MINE_COUNT);

    public static void StartGame()
    {
        GameLoop(map);
    }

    private static void GameLoop(Map map)
    {
        // Start the loop
        IsGameRunning = true;

        // Render the empty field
        Render();

        while (IsGameRunning && !map.IsMapClear)
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
        map.PrepareRender();

        BufferedRenderer.Render();
    }

    private static void GameEnd()
    {
        // The game didn't end violently (the player won)
        if (map.IsMapClear)
            BufferedRenderer.AddToAdditional(new("You win!"));

        Render();
    }
}