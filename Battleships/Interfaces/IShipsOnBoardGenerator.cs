using System.Collections.Generic;

namespace Battleships.Interfaces
{
    public interface IShipsOnBoardGenerator
    {
        List<string> PlaceShipsOnBoard(int boardSize, List<int> shipSizes);
    }
}