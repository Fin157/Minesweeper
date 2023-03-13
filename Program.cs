using Minesweeper.Commands;
using Minesweeper.Gameplay;
using Minesweeper.Input;
using Minesweeper.Rendering;

namespace Minesweeper;

internal class Program
{
    private const int MAP_SIZE_X = 20;
    private const int MAP_SIZE_Y = 20;
    private const int MINE_COUNT = 25;
    private const int UNCOVERED_STARTING_TILES_MIN = 10;
    private const int UNCOVERED_STARTING_TILES_MAX = 10;

    public static bool IsGameRunning { get; set; }

    private static readonly Map map = Map.GenerateMap(MAP_SIZE_X, MAP_SIZE_Y, MINE_COUNT);

    private static void Main(string[] args)
    {
        // Initiate the game loop
        IsGameRunning = true;
        GameLoop(map);
    }

    private static void GameLoop(Map map)
    {
        // Render the empty field
        MapRenderer.Render(map);

        while (IsGameRunning)
        {
            // Gather user input
            if (!InputManager.TakeInput(out Command? command, out string[] commandData) || command == null)
                return;

            // Process the input
            bool isRenderNeeded = ProcessInput(map, command, commandData);

            // Render the changes if needed
            if (isRenderNeeded)
                MapRenderer.Render(map);
        }
    }

    private static bool ProcessInput(Map map, Command command, string[] commandData)
    {
        // Execute the chosen command on the specified tile
        return command.Execute(map, commandData);
    }
}