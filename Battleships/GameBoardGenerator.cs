using Battleships.Models;
using Battleships.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Battleships.Interfaces;

namespace Battleships
{
    public class GameBoardGenerator: IGameBoardGenerator
    {
        private readonly IShipGenerator _shipGenerator;

        public GameBoardGenerator(IShipGenerator shipGenerator)
        {
            _shipGenerator = shipGenerator;
        }

        public IGameBoard GenerateBoardGame(int boardSize, List<int> shipSizes)
        {
            throw new NotImplementedException();
        }
    }
}
