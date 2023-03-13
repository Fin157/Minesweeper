using Minesweeper.Gameplay;

namespace Minesweeper.Rendering;

internal static class MapRenderer
{
    public static void Render(Map map)
    {
        Console.Clear();

        // Controls if the next tile is going to be drawn a lighter or darker variant to
        // distinct neighbour tiles from each other
        bool isDarker = false;

        // Render the first row of the table (header row)
        for (int x = 0; x <= map.LengthX; x++)
        {
            string headerText = x % 2 != 0 ? x.ToString("00") : "  ";
            Console.Write(headerText);
        }
        Console.WriteLine();

        // Render the map (data rows of the table)
        for (int y = 0; y < map.LengthY; y++)
        {
            string headerText = y % 2 == 0 ? (y + 1).ToString("00") : "  ";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(headerText);

            for (int x = 0; x < map.LengthX; x++)
            {
                map[x, y].Render();
                isDarker = !isDarker;
            }

            Console.WriteLine();
            isDarker = !isDarker;
        }

        Console.BackgroundColor = ConsoleColor.Black;
    }
}