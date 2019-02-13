using System;
using System.Collections.Generic;
using System.Text;
using Battleships.Interfaces;

namespace Battleships.Models
{
    public class GameBoard: IGameBoard
    {
        private readonly Dictionary<string, FieldStatus> _boardFields;
        
        public GameBoard()
        {
            _boardFields = new Dictionary<string, FieldStatus>();
        }

        public GameBoard(List<string> shipsPositions)
        {
            _boardFields = new Dictionary<string, FieldStatus>();
            foreach (var position in shipsPositions)
            {
                _boardFields.Add(position, FieldStatus.Ship);
            }
        }

        public BoardFields GetCurrentBoardState()
        {
            return new BoardFields(_boardFields);
        }

        public bool AreAllShipSunk()
        {
            return !_boardFields.ContainsValue(FieldStatus.Ship);
        }

        public void ProcessUserShot(string fieldName)
        {
            if (_boardFields.ContainsKey(fieldName))
                ProcessNotEmptyField(fieldName);
            else
                MarkEmptyFieldAsHit(fieldName);

        }

        private void MarkEmptyFieldAsHit(string fieldName)
        {
            _boardFields.Add(fieldName, FieldStatus.Shooted);
        }

        private void ProcessNotEmptyField(string fieldName)
        {
            var fieldStatus = _boardFields[fieldName];
            if (fieldStatus == FieldStatus.Ship)
                _boardFields[fieldName] = FieldStatus.ShipHit;
        }
    }
}
