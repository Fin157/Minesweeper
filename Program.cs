using Minesweeper.Commands;
using Minesweeper.Gameplay;
using Minesweeper.Input;

namespace Minesweeper;

internal class Program
{
    private const int MAP_SIZE_X = 20;
    private const int MAP_SIZE_Y = 20;
    private const int MINE_COUNT = 25;

    private static void Main(string[] args)
    {
        Map map = Map.GenerateMap(MAP_SIZE_X, MAP_SIZE_Y, MINE_COUNT);

        // Initiate the game loop
        GameLoop(map);
    }

    private static void GameLoop(Map map)
    {
        // Gather user input
        if (!InputManager.TakeInput(out ICommand? command, out string[] commandData) || command == null)
            return;

        // Process the input
        ProcessInput(map, command, commandData);

        // Render the changes
        Render();
    }

    private static void ProcessInput(Map map, ICommand command, string[] commandData)
    {
        // Execute the chosen command on the specified tile
        command.Execute(map, commandData);
    }

    private static void Render()
    {
        Console.Clear();
    }
}