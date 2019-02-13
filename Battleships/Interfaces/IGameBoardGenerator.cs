using System.Collections.Generic;

namespace Battleships.Interfaces
{
    public interface IGameBoardGenerator
    {
        IGameBoard GenerateBoardGame(int boardSize, List<int> shipSizes);
    }
}