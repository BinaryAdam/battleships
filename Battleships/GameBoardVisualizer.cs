using Battleships.Interfaces;
using Battleships.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class GameBoardVisualizer : IGameBoardVisualizer
    {
        private const string HIT_MARK = "o";
        private const string EMPTY_FIELD = "-";
        private const string MISS_MARK = "x";
        private StringBuilder _stringBuilder;
        private int _boardSize;
        private BoardFields _fields;       


        public string GetBoardStateVisualized(int boardSize, BoardFields boardFields)
        {
            _stringBuilder = new StringBuilder();
            _boardSize = boardSize;
            _fields = boardFields;
            GenerateColumnHeader();
            for (int i = 0; i < boardSize; i++)
            {
                GenerateRow(i);
            }

            return _stringBuilder.ToString();
        }

        private void GenerateRow(int rowNumber)
        {
            var rowLetter = (char)('A' + rowNumber);
            _stringBuilder.Append(rowLetter);
            _stringBuilder.Append(" ");
            for (int i = 0; i < _boardSize; i++)
            {
                var fieldName = $"{rowLetter}{i + 1}";
                if (!_fields.ContainsKey(fieldName))
                    _stringBuilder.Append(EMPTY_FIELD + " ");
                else
                    VisualizeNotEmptyField(_fields[fieldName]);

            }
            _stringBuilder.AppendLine();
        }

        private void VisualizeNotEmptyField(FieldStatus fieldStatus)
        {
            var fieldMark = "";
            switch(fieldStatus)
            {
                case FieldStatus.Empty:
                case FieldStatus.Ship: fieldMark = EMPTY_FIELD; break;
                case FieldStatus.ShipHit: fieldMark = HIT_MARK; break;
                case FieldStatus.Shooted: fieldMark = MISS_MARK; break;
            }
            _stringBuilder.Append($"{fieldMark} ");
        }

        private void GenerateColumnHeader()
        {
            _stringBuilder.Append("  ");
            for (int i = 0; i < _boardSize; i++)
            {
                _stringBuilder.Append($"{i + 1} ");
            }
            _stringBuilder.AppendLine(" x - miss o - hit");
        }
    }
}
