﻿using Minesweeper.Gameplay;

namespace Minesweeper.Commands;

internal class DigCommand : ICommand
{
    public void Execute(Map map, string[] userInput)
    {
        //map[pos.x, pos.y].IsUncovered = true;
    }
}
