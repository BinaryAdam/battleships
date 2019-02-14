using Battleships.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Interfaces
{
    public interface IGameBoardVisualizer
    {
        string GetBoardStateVisualized(int boardSize, BoardFields boardFields); 
    }
}
