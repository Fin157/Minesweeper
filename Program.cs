using Minesweeper.Commands;
using Minesweeper.Gameplay;
using Minesweeper.Input;

namespace Minesweeper;

internal class Program
{
    private const ConsoleColor COLOUR_LIGHT = ConsoleColor.Green;
    private const ConsoleColor COLOUR_DARK = ConsoleColor.DarkGreen;
    private const int MAP_SIZE_X = 20;
    private const int MAP_SIZE_Y = 20;
    private const int MINE_COUNT = 25;

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
        Render();

        while (IsGameRunning)
        {
            // Gather user input
            if (!InputManager.TakeInput(out ICommand? command, out string[] commandData) || command == null)
                return;

            // Process the input
            ProcessInput(map, command, commandData);

            // Render the changes
            Render();
        }
    }

    private static void ProcessInput(Map map, ICommand command, string[] commandData)
    {
        // Execute the chosen command on the specified tile
        command.Execute(map, commandData);
    }

    private static void Render()
    {
        Console.Clear();

        // Controls if the next tile is going to be drawn a lighter or darker variant to
        // distinct neighbour tiles from each other
        bool isDarker = false;

        for (int y = 0; y < map.tiles.GetLength(0); y++)
        {
            string mapLine = "";

            for (int x = 0; x < map.tiles.GetLength(1); x++)
            {
                Tile t = map.tiles[x, y];
                string tileText;
                if (!t.IsUncovered) // If the tile hasn't been uncovered yet
                {
                    if (t.IsMarked)
                        tileText = "--";
                    else
                        tileText = "  ";
                }
                else
                    tileText = (t as ClearTile)?.MinesAround + " ";

                Console.BackgroundColor = isDarker ? COLOUR_DARK : COLOUR_LIGHT;
                mapLine += tileText;
                isDarker = !isDarker;
            }

            BufferedRenderer.AddLine(mapLine);
            isDarker = !isDarker;
        }

        Console.BackgroundColor = ConsoleColor.Black;

        BufferedRenderer.Render();
    }
}