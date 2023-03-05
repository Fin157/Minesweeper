namespace Minesweeper;

internal class Program
{
    private const int MAP_SIZE_X = 20;
    private const int MAP_SIZE_Y = 20;
    private const int MINE_COUNT = 25;

    private static void Main(string[] args)
    {
        Tile[,] map = MapGenerator.GenerateMap(MAP_SIZE_X, MAP_SIZE_Y, MINE_COUNT);

        // Initiate the game loop
        GameLoop(map);
    }

    private static void GameLoop(Tile[,] map)
    {
        // Gather user input
        Input userInput = TakeInput();

        // Process the input
        ProcessInput(userInput);

        // Render the changes
        Render();
    }

    private static Input TakeInput()
    {
        // Get the position of the affected tile

        // Get the command type to be executed on the chosen tile
    }

    private static void ProcessInput(Input userInput)
    {
        // Execute the chosen command on the specified tile
    }

    private static void Render()
    {
        Console.Clear();
    }
}