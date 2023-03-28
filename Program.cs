using Minesweeper.Gameplay;
using Minesweeper.Input;

namespace Minesweeper;

internal static class Program
{
    public const int MAP_SIZE_X = 6;
    public const int MAP_SIZE_Y = 12;
    public const int MINE_COUNT = 10;

    public static List<Game> sims = new();

    public static void Main(string[] args)
    {
        sims.Add(new(new UserInputManager(), MAP_SIZE_X, MAP_SIZE_Y, MINE_COUNT, true));
    }
}
