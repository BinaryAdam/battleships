using Battleships.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class BattleshipsGame
    {
        private readonly IGameBoardVisualizer _boardVisualizer;
        private readonly IInputReader _inputReader;
        private readonly IContentWriter _writer;
        private readonly IInputValidator _validator;
        private IGameBoard _gameBoard;
        private bool _isGameEnded;
        private bool _isGameInterupted;
        private int _boardSize;
        private string _endGameKey = "Q";

        public BattleshipsGame(IGameBoardVisualizer boardVisualizer,
            IInputReader inputReader, 
            IContentWriter writer,
            IInputValidator validator)
        {
            _boardVisualizer = boardVisualizer;
            _inputReader = inputReader;
            _writer = writer;
            _validator = validator;
        }

        public void PlayGameToEnd(IGameBoard gameBoard, int boardSize)
        {
            _gameBoard = gameBoard;
            _boardSize = boardSize;
            _isGameEnded = _isGameInterupted = false;

            do
            {
                PlayRound();
            } while (!_isGameEnded);

            if (!_isGameInterupted)
                _writer.WriteLine("Congratatulations!!! You won!!!");
            _writer.WriteLine("Press any key to close app....");
        }

        private void PlayRound()
        {
            WriteBoardState();
            var input = GetInput();
            if(input.Equals(_endGameKey))
            {
                _isGameEnded = _isGameInterupted = true;
                return;
            }
            _gameBoard.ProcessUserShot(input);
            _isGameEnded = _gameBoard.AreAllShipSunk();
        }

        private string GetInput()
        {
            var input = string.Empty;
            do
            {
                input = _inputReader.ReadInput().ToUpper();

            } while (!input.Equals(_endGameKey) && !_validator.IsInputValid(input));
            return input;
        }

        private void WriteBoardState()
        {
            _writer.ClearScreen();
            var fields = _gameBoard.GetCurrentBoardState();
            var currentBoard = _boardVisualizer.GetBoardStateVisualized(_boardSize, fields);
            _writer.WriteLine(currentBoard);
            _writer.WriteLine("Enter next shot coordinates e.g 'A1' or 'J10' ('q' to quit):");
        }
    }
}
