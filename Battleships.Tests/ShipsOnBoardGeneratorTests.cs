using Battleships;
using Battleships.Interfaces;
using Battleships.Utils;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Tests
{
    public class IShipsOnBoardGeneratorTests
    {
        private readonly int _boardSize = 10;
        private readonly List<int> _shipsSizes = new List<int> { 1, 1 };
        private readonly List<string> _firstShip = new List<string> { "A1" };
        private readonly List<string> _firstShipOutOfBounds = new List<string> { "A11" };
        private readonly List<string> _secondShip = new List<string> { "B3" };
        private readonly List<string> _secondShipCrossedWithFirstShip = new List<string> { "A1" };

        [Test]
        public void GenerateBoardGame_ShouldReturnValidShipFieldsCount()
        {
            var _shipGeneratorMock = new Mock<IShipGenerator>();
            _shipGeneratorMock.SetupSequence(sg => sg.GenerateShipFields(_firstShip.Count))
                .Returns(_firstShip)
                .Returns(_secondShip);
            
            var validator = SetupValidator();
            var sut = new ShipsOnBoardGenerator(_shipGeneratorMock.Object, validator);

            var result = sut.PlaceShipsOnBoard(_boardSize, _shipsSizes);

            Assert.That(result, Has.Exactly(_shipsSizes.Sum()).Items);
        }

        [Test]
        public void GenerateBoardGame_ShouldPlaceOnlyShipsInBoardBounces()
        {
            var _shipGeneratorMock = new Mock<IShipGenerator>();
            _shipGeneratorMock.SetupSequence(sg => sg.GenerateShipFields(_firstShip.Count))
                .Returns(_firstShipOutOfBounds)
                .Returns(_firstShip);
            var validatorMock = new Mock<IInputValidator>();
            validatorMock.Setup(v => v.IsInputValid(_firstShipOutOfBounds.First())).Returns(false);
            validatorMock.Setup(v => v.IsInputValid(_firstShip.First())).Returns(true);

            var sut = new ShipsOnBoardGenerator(_shipGeneratorMock.Object, validatorMock.Object);

            var result = sut.PlaceShipsOnBoard(_boardSize, new List<int> { _firstShip.Count });

            Assert.That(_firstShipOutOfBounds, Is.Not.SubsetOf(result));
            Assert.That(_firstShip, Is.SubsetOf(result));
            
        }

        [Test]
        public void GenerateBoardGame_ShouldReturnOnlyNotCrossedShips()
        {
            var _shipGeneratorMock = new Mock<IShipGenerator>();
            _shipGeneratorMock.SetupSequence(sg => sg.GenerateShipFields(_firstShip.Count))
                .Returns(_firstShip)
                .Returns(_secondShipCrossedWithFirstShip)
                .Returns(_secondShip);
            var validator = SetupValidator();
            var sut = new ShipsOnBoardGenerator(_shipGeneratorMock.Object, validator);

            var result = sut.PlaceShipsOnBoard(_boardSize, _shipsSizes);

            Assert.That(result, Is.Unique);
        }

        private IInputValidator SetupValidator()
        {
            var validatorMock = new Mock<IInputValidator>();
            validatorMock.Setup(v => v.IsInputValid(It.IsAny<string>())).Returns(true);
            return validatorMock.Object;
        }
    }
}