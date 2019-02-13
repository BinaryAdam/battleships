using Battleships.Models;
using Battleships.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Battleships.Interfaces;

namespace Battleships
{
    public class ShipsOnBoardGenerator: IShipsOnBoardGenerator
    {
        private readonly IShipGenerator _shipGenerator;
        private readonly IInputValidator _inputValidator;
        private List<string> _shipsFields;
        private int _boardSize;

        public ShipsOnBoardGenerator(IShipGenerator shipGenerator, IInputValidator inputValidator)
        {
            _shipGenerator = shipGenerator;
            _inputValidator = inputValidator;
        }

        public List<string> PlaceShipsOnBoard(int boardSize, List<int> shipSizes)
        {
            _shipsFields = new List<string>();
            _boardSize = boardSize;
            foreach (var shipSize in shipSizes)
            {
                PlaceShipOnBoard(shipSize);
            }
            return _shipsFields;
        }

        private void PlaceShipOnBoard(int shipSize)
        {
            List<string> shipFields;
            do
            {
                shipFields = _shipGenerator.GenerateShipFields(shipSize);
            } while (ShipCannotBePlacedOnBoard(shipFields));

            _shipsFields.AddRange(shipFields);
        }

        private bool ShipCannotBePlacedOnBoard(List<string> shipFields)
        {
            foreach (var field in shipFields)
            {
                if (FieldIsOutOfBounds(field))
                    return true;
                if (FieldOverlapseExistingShip(field))
                    return true;
            }
            return false;
        }

        private bool FieldOverlapseExistingShip(string field)
        {
            return _shipsFields.Contains(field);
        }

        private bool FieldIsOutOfBounds(string field)
        {
            return !_inputValidator.IsInputValid(field);
        }
    }
}
