using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Models
{
    public class GameBoard
    {
        private Dictionary<string, FieldStatus> BoardFields;

        public GameBoard(List<string> shipsPositions)
        {
            BoardFields = new Dictionary<string, FieldStatus>();
            foreach (var position in shipsPositions)
            {
                BoardFields.Add(position, FieldStatus.Ship);
            }
        }

        public Dictionary<string, FieldStatus> GetCurrentBoardState()
        {
            return new Dictionary<string, FieldStatus>(BoardFields);
        }

        public bool AreAllShipSunk()
        {
            return !BoardFields.ContainsValue(FieldStatus.Ship);
        }

        public void MarkFieldAsHit(string fieldName)
        {
            if (BoardFields.ContainsKey(fieldName))
                ProcessNotEmptyField(fieldName);
            else
                MarkEmptyFieldAsHit(fieldName);

        }

        private void MarkEmptyFieldAsHit(string fieldName)
        {
            BoardFields.Add(fieldName, FieldStatus.Shooted);
        }

        private void ProcessNotEmptyField(string fieldName)
        {
            var fieldStatus = BoardFields[fieldName];
            if (fieldStatus == FieldStatus.Ship)
                BoardFields[fieldName] = FieldStatus.ShipHit;
        }
    }
}
