using Battleships.Interfaces;
using Battleships.Models;
using Battleships.Utils;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    class Program
    {
        static readonly Container _container;

        static Program()
        {

            _container = new Container();
            SetUpDependencyInjection();

            _container.Verify();
        }

        static void Main(string[] args)
        {
            var shipsOnBoardGenerator = _container.GetInstance<IShipsOnBoardGenerator>();
            var game = _container.GetInstance<BattleshipsGame>();

            var boardSize = 10;
            var shipsSizes = new List<int> { 5, 4, 4 };

            var initialShipPositions = 
                shipsOnBoardGenerator.PlaceShipsOnBoard(boardSize, shipsSizes);
            var boardGame = new GameBoard(initialShipPositions);

            game.PlayGameToEnd(boardGame, boardSize);

            Console.ReadLine();
        }



        private static void SetUpDependencyInjection()
        {
            _container.Register<IContentWriter, ContentWriter>();
            _container.Register<IGameBoardVisualizer, GameBoardVisualizer>();
            _container.Register<IInputReader, InputReader>();
            _container.Register<IInputValidator, InputValidator>();
            _container.Register<IRandomGenerator, RandomGenerator>();
            _container.Register<IShipGenerator, ShipGenerator>();
            _container.Register<IShipsOnBoardGenerator, ShipsOnBoardGenerator>();
            _container.Register<BattleshipsGame>();
        }
    }
}
