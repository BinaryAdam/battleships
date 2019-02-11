using Battleships.Models;
using Battleships.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class GameBoardGenerator
    {
        private readonly IRandomGenerator _random;
        private readonly int _boardSize = 10;
        
        public GameBoardGenerator(IRandomGenerator random)
        {
            _random = random;
        }
        public GameBoard GenerateGameBoard(List<int> shipsSizes)
        {
            var board = new GameBoard();
           
            return board;
        }

        
    }
}
