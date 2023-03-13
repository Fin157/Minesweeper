﻿using Minesweeper.Commands;
using Minesweeper.Gameplay;
using Minesweeper.Input;
using Minesweeper.Rendering;

namespace Minesweeper;

internal class Program
{
    private const int MAP_SIZE_X = 20;
    private const int MAP_SIZE_Y = 20;
    private const int MINE_COUNT = 100;

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
            Command? command = InputManager.TakeInput(out string[] commandData);

            // Process the input
            bool isRenderNeeded = ProcessInput(map, command, commandData);

            // Render the changes if needed
            if (isRenderNeeded)
                MapRenderer.Render(map);
        }
    }

    private static bool ProcessInput(Map map, Command? command, string[] commandData)
    {
        // Execute the chosen command on the specified tile (if it is not null)
        // and return the result of the command's execute method
        return command == null || command.Execute(map, commandData);
    }
}