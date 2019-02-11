using Battleships.Interfaces;
using Battleships.Models;
using Battleships.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class ShipGenerator : IShipGenerator
    {
        private readonly IRandomGenerator _random;
        private readonly int _boardSize = 10;
        private readonly int _numberOfDirections = 2;
        private readonly int _columnOffset = 1;

        public ShipGenerator(IRandomGenerator random)
        {
            _random = random;
        }

        public List<string> GenerateShipFields(int shipSize)
        {            
            var startColumn = _random.GetRandomNumber(_boardSize) + _columnOffset;
            var startRow = _random.GetRandomNumber(_boardSize);
            var direction = (ShipDirection)_random.GetRandomNumber(_numberOfDirections);

            if (direction == ShipDirection.Column)
                return GenerateFieldsInColumn(shipSize, startColumn, startRow);
            else
                return GenerateFieldsInRow(shipSize, startColumn, startRow);
            
        }

        private List<string> GenerateFieldsInRow(int shipSize, int startColumn, int startRow)
        {
            var fields = new List<string>();
            var rowLetter = GetRowLetter(startRow);
            for (int i = 0; i < shipSize; i++)
            {
                fields.Add($"{rowLetter}{startColumn + i}");
            }
            return fields;
        }

        private List<string> GenerateFieldsInColumn(int shipSize, int startColumn, int startRow)
        {
            var fields = new List<string>();
            for (int i = 0; i < shipSize; i++)
            {
                fields.Add($"{GetRowLetter(startRow + i)}{startColumn}");
            }
            return fields;
        }

        private string GetRowLetter(int startRow)
        {
            return ((char)(65 + startRow)).ToString();
        }
    }
}
